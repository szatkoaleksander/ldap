namespace LDAP.User
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public User()
        {
            Username = "test";
            Password = "test";
        }
    }
}