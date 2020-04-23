﻿using System;
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
                    throw new Exception("User already exisit");
                }
                if (users.Value.GetNickname().Equals(nickname))
                {
                    throw new Exception("Nickname already taken");
                }
                if (string.IsNullOrWhiteSpace(nickname))
                {
                    throw new Exception("Invalid nickname");
                }

            }
            if(!IsValidEmail(email))
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
        }

        public void Login(string email, string pass)
        {
             //email = email.ToLower();
            
             if (users.ContainsKey(email)) // if user does not exisit
             {
                    throw new Exception("User does not exisit");
             }
     
             if (!users[email].Login(pass)) /// is correct password
             {
                throw new Exception("Can't logged in");
             }
            loggedInUser = users[email];
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

            if (pass.Length < 4 && pass.Length > 20)
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
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        /*public bool IsValidEmail1(string email)
        {
            if (!email.Contains("."))
            {
                return false;
            }
            int count = 0;
            foreach (char c in email)
            {
                if (c.Equals("@"))
                    count++;
            }
            if (count == 1)
                return true;
            return false;
        }
        */
    }
}
