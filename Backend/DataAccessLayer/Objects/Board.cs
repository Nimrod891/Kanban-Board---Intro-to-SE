using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Objects
{
    public class Board : DALObject<Board>
    {
        private string email; //{ get; set; }
        private Column[] columns;

        public Board(string email)
        {
            this.email = email;
            columns = new Column[3];
            Column backLog = new Column("BackLog", 0);
            Column inProgress = new Column("InProgress", 1);
            Column done = new Column("Done", 2);
            columns[0] = backLog;
            columns[1] = inProgress;
            columns[2] = done;
            
        }

        public Board()
        {
        }

        public DALObject<Board> Import(string email)
        {
            return DALObject<Board>.FromJson(DALController.Read(this.GetSafeFilename(email) + ".json"));
        }
        public override void Save()
        {
            DALController.Write(Path.Combine("Boards", this.GetSafeFilename(email)+ ".json"), this.ToJson());

        }

        public override string ToString()
        { return ("Email: " + ", nickname: " + ", pass: "); }

        public string getEmail()
        {
            return this.email;
        }
    }
}