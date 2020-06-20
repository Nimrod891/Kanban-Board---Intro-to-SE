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
        private string myBoardHostMail;
        DataAccessLayer.BoardDalController myBoardDC = new DataAccessLayer.BoardDalController();

        public User(string email, string password, string nickname)
        {
            this.email = email;
            this.password = password;
            this.nickname = nickname;
            this.is_logged = false;
            myBoard = new BoardPackage.Board(email);
        }
        public User(string email, string password, string nickname, string emailHost)// not host user
        {
            this.email = email;
            this.password = password;
            this.nickname = nickname;
            this.is_logged = false;
            myBoardHostMail = emailHost;
        }
        public User(DataAccessLayer.DTOs.UserDTO u)
        {
            this.email = u.email;
            this.password = u.Password;
            this.nickname = u.NickName;
            this.is_logged = false;
            DataAccessLayer.DTOs.BoardDTO DBoard = myBoardDC.Select(u.email);
            myBoard = new BoardPackage.Board(0,DBoard.email);
            //myBoard.initBoard();
        }
        
        public BoardPackage.Board getMyBoard()
        {
            return this.myBoard;
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
                myBoard.setLoggedInUser(this.email);
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
        public void setMyBoard(BoardPackage.Board b)
        {
            this.myBoard = b;
        }
    }
}
