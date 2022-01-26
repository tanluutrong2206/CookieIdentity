using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookieIdentity.Pages
{
    [Authorize(Policy = "AdminOnly")]
    public class ConfigurationSystemModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
