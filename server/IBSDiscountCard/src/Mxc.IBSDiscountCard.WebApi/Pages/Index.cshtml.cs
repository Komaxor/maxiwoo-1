using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Mxc.IBSDiscountCard.WebApi.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModelBase
    {
        public void OnGet()
        {
        }
    }
}