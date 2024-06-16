using System.Collections.Generic;
using System.Linq;
using YourNamespace.Models;

namespace YourNamespace.Services
{
    public class UserService
    {
        private List<User> _users = new List<User>
        {
            new User { Username = "admin", Password = "admin", Role = Roles.Admin },
            new User { Username = "manager", Password = "manager", Role = Roles.Manager },
            new User { Username = "user", Password = "user", Role = Roles.User }
        };

        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(u => u.Username == username && u.Password == password);
            return user;
        }

        public User GetUserByUsername(string username)
        {
            return _users.SingleOrDefault(u => u.Username == username);
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public void RemoveUser(string username)
        {
            var userToRemove = _users.SingleOrDefault(u => u.Username == username);
            if (userToRemove != null)
            {
                _users.Remove(userToRemove);
            }
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }
    }
}
