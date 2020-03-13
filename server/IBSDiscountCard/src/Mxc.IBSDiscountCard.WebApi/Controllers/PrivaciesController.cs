using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;

namespace Mxc.IBSDiscountCard.WebApi.Controllers
{
    public class PrivaciesController : Controller
    {
        /// <summary>
        /// Get terms of use
        /// </summary>
        /// <returns>Terms Of Use in HTML format</returns>
        [HttpGet("/termsofuse")]
        public IActionResult TermsOfUse()
        {
            return View("TermsOfUse");
        }
        
        /// <summary>
        /// Get privacy policy
        /// </summary>
        /// <returns>Privacy policy in html format</returns>
        [HttpGet("/privacypolicy")]
        public IActionResult PrivacyPolicy()
        {
            return View("PrivacyPolicy");
        }
    }
}