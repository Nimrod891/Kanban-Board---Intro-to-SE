using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserPackage
{
    class UserController
    {
        private Dictionary<string, User> users;
        private User loggedInUser;

        public UserController()
        {
            users = new Dictionary<string, User>();

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Kanban JSON Files", "Users");
            Directory.CreateDirectory(path);
            foreach (string file in Directory.EnumerateFiles(path, "*.json"))
            {
                User userToAdd = new User(DataAccessLayer.Objects.User.FromJson(file));
                //userToAdd = DataAccessLayer.Objects.User.FromJson(Read(file));
                users.Add(userToAdd.GetEmail(), userToAdd);

                /// if users exist in the folder /Kanban JSON Files/Users 
                /// this will create a dictionary of {email, user} as a field in UserController

            }
            this.loggedInUser = null;


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
