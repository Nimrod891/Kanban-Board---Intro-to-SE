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
        public string email { get; set; }
        public string password { get; set; }
        public string nickname { get; set; }

        public User(string email, string password, string nickname)
        {
            this.email = email;
            this.password = password;
            this.nickname = nickname;
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

    }
}
