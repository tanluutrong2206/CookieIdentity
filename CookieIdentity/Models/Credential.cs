using System.ComponentModel.DataAnnotations;

namespace CookieIdentity.Models
{
    public class Credential
    {
        public Credential()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
