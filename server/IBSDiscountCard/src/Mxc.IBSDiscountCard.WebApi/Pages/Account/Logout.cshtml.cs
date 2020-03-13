using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.User;

namespace Mxc.IBSDiscountCard.WebApi.Pages.Account
{
    public class Logout : PageModelBase
    {
        private readonly SignInManager<UserDb> _signInManager;

        public Logout(SignInManager<UserDb>signInManager)
        {
            _signInManager = signInManager;
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            await _signInManager.SignOutAsync();

            return RedirectToPage("/Index");
        }
    }
}