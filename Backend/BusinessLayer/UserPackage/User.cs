using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserPackage
{
    class User 
    {
        private string email;
        private string password;
        private string nickname;
        private bool is_logged;
        private BoardPackage.Board myBoard;
        DataAccessLayer.BoardDalController myBoardDC;

        public User(string email, string password, string nickname)
        {
            this.email = email;
            this.password = password;
            this.nickname = nickname;
            this.is_logged = false;
            myBoard = new BoardPackage.Board(email);
        }
        public User(DataAccessLayer.DTOs.UserDTO u)
        {
            this.email = u.email;
            this.password = u.Password;
            this.nickname = u.NickName;
            this.is_logged = false;
            List<DataAccessLayer.DTOs.BoardDTO> myInitBoard = myBoardDC.Select(0, email);


        }
        

        public string GetEmail()
        {
            return email;
        }

        public string GetPass()
        {
            return password;
        }

        public string GetNickname()
        {
            return nickname;
        }
        public BoardPackage.Board GetUserBoard()
        {
            return myBoard;
        }


        public bool Login(string pass)
        {
            if (this.password.Equals(pass))
            {
                is_logged = true;
                myBoard.SetIsULoggedIn(true);
                return true;
            }
            return false;
        }

        public void Logout()
        {
            if (!is_logged)
            {
                throw new Exception("You're already logged out");
            }
            is_logged = false;
            myBoard.SetIsULoggedIn(false);
        }

    }
}
