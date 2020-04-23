using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Objects
{
    public class User : DALObject<User>
    {
        private string email { get; set; }
        private string password { get; set; }
        private string nickname { get; set; }
        private Board myBoard;

        public User(string email, string password, string nickname)
        {
            this.email = email;
            this.password = password;
            this.nickname = nickname;
            this.myBoard = new Board();
        }

        public User()
        {
        }

        public DALObject<User> Import(string email)
        {
            return DALObject<User>.FromJson(DALController.Read(this.GetSafeFilename(email) + ".json"));
        }
        public override void Save()
        {
            DALController.Write(this.GetSafeFilename(email) + ".json", this.ToJson());
           
        }

        public override string ToString()
        { return ("Email: "+this.email+", nickname: "+this.nickname+", pass: "+this.password);}

    }
}
