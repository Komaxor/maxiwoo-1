using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mxc.Helpers.Errors;
using Mxc.IBSDiscountCard.Application.User.Services;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore.Update;
using Mxc.IBSDiscountCard.Application.User.Queries;
using Mxc.IBSDiscountCard.Domain.InstituteAggregate;
using Mxc.IBSDiscountCard.Domain.UserAggregate;
using Mxc.IBSDiscountCard.Infrastructure.Repositories;

namespace Mxc.IBSDiscountCard.Infrastructure.Services
{
    public class AspIdentityLoginManager : UserManager<UserDb>, ILoginManager
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<UserDb> _signInManager;
        private readonly IdentityOptions _options;
        private readonly JWTOption _jwtOptions;
        private readonly EmailOptions _loginOptions;
        private readonly ApplicationDbContext _db;
        private readonly PolicyOptions _policyOptions;
        private readonly ILogger<AspIdentityLoginManager> _logger;

        public AspIdentityLoginManager(IMapper mapper,
            SignInManager<UserDb> signInManager,
            ApplicationDbContext db,
            IUserStore<UserDb> store,
            IOptions<IdentityOptions> optionsAccessor,
            IOptions<JWTOption> jwtOptions,
            IOptions<EmailOptions> loginOptions,
            IOptions<PolicyOptions> policyOptions,
            IPasswordHasher<UserDb> passwordHasher,
            IEnumerable<IUserValidator<UserDb>> userValidators,
            IEnumerable<IPasswordValidator<UserDb>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<AspIdentityLoginManager> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors,
                services, logger)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _options = optionsAccessor.Value;
            _jwtOptions = jwtOptions.Value;
            _loginOptions = loginOptions.Value;
            _db = db;
            _policyOptions = policyOptions.Value;
            _logger = logger;
        }

        public async Task RegistrationAsync(User user, string role = Roles.Customer)
        {
            var userDb = _mapper.Map<UserDb>(user);

            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                var res = await CreateAsync(userDb, user.Password);

                if (!res.Succeeded)
                {
                    transaction.Rollback();
                    if (res.Errors.First().Code.StartsWith("Password"))
                    {
                        throw new IBSDiscountCardApiErrorException(FunctionCodes.WeakPassword,
                            new ApiErrorLabel(res.Errors.First().Code));
                    }

                    if (res.Errors.First().Code == "InvalidUserName")
                    {
                        throw new IBSDiscountCardApiErrorException(FunctionCodes.InvalidUserName,
                            new ApiErrorLabel(res.Errors.First().Code));
                    }

                    if (res.Errors.First().Code == "DuplicateUserName")
                    {
                        throw new IBSDiscountCardApiErrorException(FunctionCodes.DuplicateUserName,
                            new ApiErrorLabel(res.Errors.First().Code));
                    }

                    throw new IBSDiscountCardApiErrorException(FunctionCodes.RegisterUserError);
                }

                var roleResult = await AddToRoleAsync(userDb, role);

                if (!roleResult.Succeeded)
                {
                    transaction.Rollback();
                    throw new IBSDiscountCardApiErrorException(FunctionCodes.UserLoginRoleNotAddedToUser);
                }

                transaction.Commit();
            }
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, true, false);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    throw new IBSDiscountCardApiErrorException(FunctionCodes.UserLoginLockedOutUser);
                }

                if (result.IsNotAllowed)
                {
                    throw new IBSDiscountCardApiErrorException(FunctionCodes.UserLoginActivationNeeded);
                }

                throw new IBSDiscountCardApiErrorException(FunctionCodes.UserLoginRefused);
            }

            return await GenerateTokenAsync(username);
        }

        private async Task<string> GenerateTokenAsync(string username)
        {
            var user = await FindByNameAsync(username);

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.UniqueName, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64),
                new Claim(IBSClaimTypes.InstituteId, user.InstitudeId.ToString(),
                    ClaimValueTypes.String)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);

            var roles = await GetRolesAsync(user);

            claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var jwt = new JwtSecurityToken(
                issuer: _options.Tokens.AuthenticatorIssuer,
                claims: claimsIdentity.Claims,
                notBefore: DateTimeOffset.Now.DateTime,
                expires: DateTimeOffset.Now.AddDays(_jwtOptions.ExpireDays).DateTime,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.SingKey)),
                    SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string GenerateCode(int lenght)
        {
            var random = new Random();
            return new string(Enumerable.Repeat(_policyOptions.CodeCharacters, lenght).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task ChangePasswordAsync(string username, string oldpassword, string newpassword)
        {
            var user = await FindByNameAsync(username);
            if (user == null)
            {
                throw new IBSDiscountCardApiErrorException(FunctionCodes.ChangePasswordUserNotFound);
            }

            if (user.PasswordChangeLockoutEnd > DateTimeOffset.Now)
            {
                throw new IBSDiscountCardApiErrorException(FunctionCodes.ChangePasswordDisabledForaWhile);
            }

            if (newpassword == oldpassword)
            {
                throw new IBSDiscountCardApiErrorException(FunctionCodes.ChangePasswordNewShouldDifferent);
            }

            var result = await ChangePasswordAsync(user, oldpassword, newpassword);

            if (!result.Succeeded)
            {
                user.PasswordChangeFailedCount++;
                if (user.PasswordChangeFailedCount >= _policyOptions.PasswordChangeMaxWrongTry)
                {
                    user.PasswordChangeLockoutEnd = DateTimeOffset.Now.AddMinutes(_policyOptions.PasswordChangeLockEndInMinutes);
                    user.PasswordChangeFailedCount = 0;
                }

                await UpdateAsync(user);
                throw new IBSDiscountCardApiErrorException(FunctionCodes.ChangePasswordError);
            }

            if (user.PasswordChangeFailedCount != 0 || user.PasswordChangeLockoutEnd != null)
            {
                user.PasswordChangeFailedCount = 0;
                user.PasswordChangeLockoutEnd = null;
                await UpdateAsync(user);
            }
        }

        public async Task<bool> SavePasswordResetCodeAsync(User user, string code)
        {
            var userdb = await FindByNameAsync(user.Email);

            if (userdb.PasswordResetLockEnd >= DateTimeOffset.Now)
            {
                _logger.LogInformation("User password reset code request refused by reset time lock.");
                throw new IBSDiscountCardApiErrorException(FunctionCodes.PasswordResetDisabledForaWhile);
            }

            userdb.PasswordResetCode = code;
            userdb.PasswordResetLockEnd = DateTimeOffset.Now.AddMinutes(_policyOptions.PasswordResetLockEndInMinutes);

            var updateresult = await UpdateAsync(userdb);
            return updateresult.Succeeded;
        }

        public async Task<bool> RemovePasswordResetCodeAsync(User user)
        {
            var userdb = await FindByNameAsync(user.Email);

            userdb.PasswordResetCode = "";
            userdb.PasswordResetLockEnd = null;

            var updateresult = await UpdateAsync(userdb);
            return updateresult.Succeeded;
        }

        public async Task<bool> ResetPasswordAsync(string newpassword, string code)
        {
            var userdb = _db.Users.FirstOrDefault(u => u.PasswordResetCode == code.ToUpper());
            if (userdb == null)
            {
                throw new IBSDiscountCardApiErrorException(FunctionCodes.SetNewPasswordInvalidCode);
            }

            var result = await UpdatePasswordHash(userdb, newpassword, true);
            if (!result.Succeeded)
            {
                if (result.Errors.First().Code.StartsWith("Password"))
                {
                    throw new IBSDiscountCardApiErrorException(FunctionCodes.SetNewPasswordWeakPassword, new ApiErrorLabel(result.Errors.First().Code));
                }

                throw new IBSDiscountCardApiErrorException(FunctionCodes.SetNewPasswordChangePasswordError);
            }

            userdb.PasswordResetCode = "";
            userdb.PasswordResetLockEnd = null;

            var updateresult = await UpdateAsync(userdb);
            return updateresult.Succeeded;
        }

        public async Task<bool> VerifyPasswordAsync(User user, string password)
        {
            var userdb = await FindByNameAsync(user.Email);

            var result = PasswordHasher.VerifyHashedPassword(userdb, userdb.PasswordHash, password);
            if (result != PasswordVerificationResult.Failed)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> HasRoleAsync(string userName, string role)
        {
            var user = await FindByNameAsync(userName);
            if (user == null)
            {
                return false;
            }
            
            var roles = await GetRolesAsync(user);
            
            return roles.Contains(role);
        }
    }
}