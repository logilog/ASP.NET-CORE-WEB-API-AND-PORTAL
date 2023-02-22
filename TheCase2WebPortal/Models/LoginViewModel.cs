using TheCase2WebPortal.Helpers;

namespace TheCase2WebPortal.Models
{
    public class LoginViewModel: ViewModelBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
