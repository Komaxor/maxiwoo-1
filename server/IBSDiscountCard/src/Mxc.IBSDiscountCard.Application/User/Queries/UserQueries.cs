using AutoMapper;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Common.LoggedInUserAccessor;
using Mxc.IBSDiscountCard.Domain.InstituteAggregate;
using Mxc.IBSDiscountCard.Domain.UserAggregate;
using Mxc.IBSDiscountCard.Domain.UserAggregate.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.Application.User.Queries
{
    public class UserQueries : IUserQueries
    {
        private readonly ILoggedInUserAccessor _loggedInUser;
        private readonly IUserRepository _userRepository;
        private readonly IInstituteRepository _instituteRepository;
        private readonly IMapper _mapper;

        public UserQueries(ILoggedInUserAccessor loggedInUser, IUserRepository userRepository, IInstituteRepository instituteRepository, IMapper mapper)
        {
            _loggedInUser = loggedInUser;
            _userRepository = userRepository;
            _mapper = mapper;
            _instituteRepository = instituteRepository;
        }

        public async Task<UserDetailsViewModel> GetMyProfileAsync()
        {
            var user = await _userRepository.GetAsync(new UserNameEquals(_loggedInUser.UserName));
            if (user == null)
            {
                throw new IBSDiscountCardApiErrorException(FunctionCodes.GetMyProfileNotFoundUser);
            }

            var institute = await _instituteRepository.GetAsync(new Domain.InstituteAggregate.Specifications.IdEquals(user.InstituteId));
            if (institute == null)
            {
                throw new IBSDiscountCardApiErrorException(FunctionCodes.GetMyProfileNotFoundInstitute);
            }

            var userVm = _mapper.Map<UserDetailsViewModel>(user);
            userVm.InstituteName = institute.Name;

            return userVm;
        }
    }
}
