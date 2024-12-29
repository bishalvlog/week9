using week9.Abstraction;
using week9.Model;
using week9.Services.Interface;

namespace week9.Services
{
    public class UserService : UserBase ,IUser
    {
        private List<User> _users;

        public const string SeedUsername = "admin";
        public const string SeedPassword = "password";

        public UserService() 
        {
            _users = LoadUsers();

            if (!_users.Any())
            {
                _users.Add(new User { UserName = SeedUsername, Password = SeedPassword });
                SaveUsers(_users);
            }

        }
        public bool Login(User user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return false; // Invalid input.
            }

            // Check if the username and password match any user in the list.
            return _users.Any(u => u.UserName == user.UserName && u.Password == user.Password);
        }
    }
}
