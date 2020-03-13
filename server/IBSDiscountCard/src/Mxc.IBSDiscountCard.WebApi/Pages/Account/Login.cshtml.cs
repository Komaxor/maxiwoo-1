using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mxc.IBSDiscountCard.Application.User.Services;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Common.LoggedInUserAccessor;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.User;
using Mxc.IBSDiscountCard.WebApi.Pages;

namespace Mxc.IBSDiscountCard.WebApi.Areas.Identity.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModelBase
    {
        private readonly ILoggedInUserAccessor _userAccessor;
        private readonly ILoginManager _loginManager;
        private readonly ILogger<LoginModel> _logger;

        [BindProperty]
        [Display(Name = "Username")]
        [Required]
        public string Username { get; set; }

        [BindProperty]
        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        public LoginModel(ILoggedInUserAccessor userAccessor, ILoginManager loginManager, ILogger<LoginModel> logger, IHostingEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.IsDevelopment())
            {
                Username = "admin@ibsdiscountcard.hu";
                Password = "sd23$asd_assgmkl23";
            }

            _userAccessor = userAccessor;
            _loginManager = loginManager;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                if (!await _loginManager.HasRoleAsync(Username, Roles.Admin))
                {
                    ModelState.AddModelError(string.Empty, "The user not exists or has no permission for this page");
                    return Page();
                }

                await _loginManager.LoginAsync(Username, Password);
            }
            catch (IBSDiscountCardApiErrorException e)
            {
                _logger.LogError(e, "Error during login");
                ModelState.AddModelError("LoginErrorLabel", e.FunctionCode.ToString());

                return Page();
            }

            _logger.LogInformation("Administrator logged in with user: " + Username);
            return RedirectToPage("/Index");
        }
    }
}