using CookieIdentity.AppCode;
using CookieIdentity.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Security.Claims;

namespace CookieIdentity.Pages.Account
{
    public class LoginModel : PageModel
    {
        public LoginModel()
        {
            Credential = new Credential
            {
                UserName = "admin",
                Password = "password"
            };
        }
        [BindProperty]
        public Credential Credential { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Credential.UserName.Trim().ToLower().Equals("admin") && Credential.Password.Equals("password"))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@gmail.com"),
                    new Claim("Admin", "true"),
                    new Claim(Constant.EMPLOYMENT_DATE, "2021-01-22"),
                };
                var identity = new ClaimsIdentity(claims, Constant.COOKIE_NAME);
                var claimPrinciple = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(Constant.COOKIE_NAME, claimPrinciple);
                return Redirect("/Index");
            } else
            {
                return Page();
            }
        }
    }
}
