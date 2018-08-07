namespace LDAP
{
    public class User
    {
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Username { get; internal set; }
        public User()
        {
        }
    }
}