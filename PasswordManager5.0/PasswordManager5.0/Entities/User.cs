using PasswordManager5._0.Functions;

namespace PasswordManager5._0.Entities
{
    public class User
    {
        public int userId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserHash { get; set; }
    }
}
