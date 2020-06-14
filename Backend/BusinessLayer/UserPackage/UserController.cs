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
        private BoardPackage.BoardController myBoardController;
        private DataAccessLayer.UserDalController myUserDC;
        private DataAccessLayer.BoardDalController myBoardDC;
        private DataAccessLayer.ColumnDalController myColumnDC;
        private DataAccessLayer.TaskDalController myTaskDC;

        public UserController()
        {
            myUserDC = new DataAccessLayer.UserDalController();
            users = new Dictionary<string, User>();
            List<DataAccessLayer.DTOs.UserDTO> myUsers = myUserDC.SelectAllusers();
            foreach (DataAccessLayer.DTOs.UserDTO u in myUsers)
            {
                User newUser = new User(u);
                users.Add(u.email, newUser);
            }
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
            User u = new User(email, pass, nickname);
            users.Add(email, u);
            DataAccessLayer.DTOs.UserDTO dataUser = new DataAccessLayer.DTOs.UserDTO(0, email, nickname, pass);
            myUserDC.Insert(dataUser);
        }
        public void Register(string email, string password, string nickname, string emailHost)// not as host
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
            if (!IsValidPass(password))
            {
                throw new Exception("Invalid password");
            }
            User u = new User(email, password, nickname, emailHost);
            users.Add(email, u);
            DataAccessLayer.DTOs.UserDTO dataUser = new DataAccessLayer.DTOs.UserDTO(0, email, nickname, password);
            myUserDC.Insert(dataUser);

        }

        public User Login(string email, string pass)
        {

            if (!users.ContainsKey(email)) // if user does not exisit
            {
                throw new Exception("User does not exisit");
            }

            if (!users[email].Login(pass)) /// is correct password
            {
                throw new Exception("Can't logged in");
            }
            loggedInUser = users[email];
            loggedInUser.getMyBoard().SetIsULoggedIn(true);
            loggedInUser.GetUserBoard().SetIsULoggedIn(true);
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
        public bool DeleteData()
        {
            myUserDC = new DataAccessLayer.UserDalController();
            myBoardDC = new DataAccessLayer.BoardDalController();
            myColumnDC = new DataAccessLayer.ColumnDalController();
            myTaskDC = new DataAccessLayer.TaskDalController();
            if (myUserDC.DeleteAll() && myBoardDC.DeleteAll() && myColumnDC.DeleteAll() && myTaskDC.DeleteAll())
            {
                return true;
            }
            return false;
        }
        public string getMyUserHostMail(string email)
        {
            foreach (var u in users)
            {
                if (u.Key.Equals(email))
                {
                    return u.Value.getMyBoard().GetUserEmail();
                }
            }
            return null;
        }
    }
}
