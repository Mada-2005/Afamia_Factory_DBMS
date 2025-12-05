namespace Afamia_UI.Models.Entities
{
    public enum Role
    {
        Employee,
        Admin,
        CEO,
    }
    public class UserAccount
    {
        
        public string Username { get; set; }
        public string Password { get; set; }
        public Role UserRole { get; set; }
        public UserAccount(int id, string username, string password, Role role)
        {
            Username = username;
            Password = password;
            UserRole = role;
        }
    }
}
