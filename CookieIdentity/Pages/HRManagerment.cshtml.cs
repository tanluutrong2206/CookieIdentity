using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookieIdentity.Pages
{
    [Authorize(Policy = "HRManager")]
    public class HRManagermentModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
