using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Objects
{ }
/*
    public class User : DALObject<User>
    {
        public string email { get; set; }
        public string password { get; set; }
        public string nickname { get; set; }
        public Board myBoard {get; set;}

        public User(string email, string password, string nickname)
        {
            this.email = email;
            this.password = password;
            this.nickname = nickname;
            this.myBoard = new Board(this.email);
        }

        public User(string email, string password, string nickname, Board myboard)
        {
            this.email = email;
            this.password = password;
            this.nickname = nickname;
            this.myBoard = myboard;
        }

        public User()
        {
        }

        public DALObject<User> Import(string email)
        {
            return DALObject<User>.FromJson(DalController.Read(Path.Combine
                ("Users", this.GetSafeFilename(email) + ".json")));
        }
        public override void Save()
        {
            DalController.Write(Path.Combine
                ("Users", this.GetSafeFilename(email) + ".json"), this.ToJson());
           
        }

        public override string ToString()
        { return ("Email: "+this.email+", nickname: "+this.nickname+", pass: "+this.password);}

    }
}
*/
