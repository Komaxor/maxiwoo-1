using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Mxc.IBSDiscountCard.WebApi.Pages
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class PageModelBase : PageModel
    {
    }
}