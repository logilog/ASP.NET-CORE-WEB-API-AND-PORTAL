namespace Entities.API
{
    public class LoginResult
    {
        public string Token { get; set; }
    }
    public class LoginInput
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
