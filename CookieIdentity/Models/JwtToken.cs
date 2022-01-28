namespace CookieIdentity.Models
{
    public class JwtToken
    {
        public JwtToken()
        {
            AccessToken = string.Empty;
        }
        public string AccessToken { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
