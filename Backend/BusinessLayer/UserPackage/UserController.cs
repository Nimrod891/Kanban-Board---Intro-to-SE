using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserPackage
{
    class UserController
    {
        private Dictionary<string, User> users;
        private User loggedInUser;
        private int maxPass = 25;
        private int minPass = 5;

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
            //this.loggedInUser = null;


        }

        public void Register(string email, string pass, string nickname)
        {
            email = email.ToLower();
            foreach (KeyValuePair<string, User> users in users) // check through dictionary if email already exisit
            {
                if (users.Key.Equals(email))
                {
                    throw new Exception("User already exisit");
                }
                if (string.IsNullOrWhiteSpace(nickname))
                {
                    throw new Exception("Invalid nickname");
                }

            }
            if (!IsValidEmail(email))
            {
                throw new Exception("Invalid email");
            }
            if (!IsValidPass(pass))
            {
                throw new Exception("Invalid password");
            }
            //email = email.ToLower();
            User u = new User(email, pass, nickname);
            users.Add(email, u);
            u.ToDalObject().Save();
            u.GetUserBoard().ToDalObject().Save();


        }

        public User Login(string email, string pass)
        {
            //email = email.ToLower();

            if (!users.ContainsKey(email)) // if user does not exisit
            {
                throw new Exception("User does not exisit");
            }

            if (!users[email].Login(pass)) /// is correct password
            {
                throw new Exception("Can't logged in");
            }
            loggedInUser = users[email];
            return loggedInUser;
        }

        public void Logout(string email)
        {
            users[email].Logout();
        }

        public bool IsValidPass(string pass)
        {
            if (string.IsNullOrWhiteSpace(pass))
            {
                return false;
            }
            bool haveUpper = false;
            bool haveDigit = false;
            bool haveLow = false;

            if (pass.Length < minPass || pass.Length > maxPass)
            {
                return false;
            }
            foreach (char c in pass)
            {

                if (char.IsUpper(c))
                {
                    haveUpper = true;
                }
                if (char.IsDigit(c))
                {
                    haveDigit = true;
                }
                if (char.IsLower(c))
                {
                    haveLow = true;
                }
            }
            if (haveUpper && haveLow && haveDigit)
            {
                return true;
            }
            return false;
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }
            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
