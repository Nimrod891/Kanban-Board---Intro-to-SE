using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserPackage
{
    class UserController
    {
        private Dictionary<string, User> users;
        private User loggedInUser;

        public UserController()
        {
            users = new Dictionary<string, User>();
        }

        public void Register(string email, string pass, string nickname)
        {
            foreach (KeyValuePair<string, User> users in users) // check through dictionary if email already exisit
            {
                if (users.Key.Equals(email))
                {
                    throw new Exception("user already exisit");
                }
            }
            User u = new User(email, pass, nickname);
            users.Add(email, u);
        }

        public void Login(string email, string pass)
        {
            if (users[email].Login(pass))
            {
                loggedInUser = users[email];
            }
            throw new Exception("can't logged in");
        }

        public void Logout(string email)
        {
            users[email].Logout();
        }
    }
}
