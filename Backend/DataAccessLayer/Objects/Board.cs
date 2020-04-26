using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Objects
{
    //[JsonObject(MemberSerialization.Fields)]
    public class Board : DALObject<Board>
    {
        public string email { get; set; }
        public Column[] columns { get; set; }
        public int taskId { get; set; }

        public Board(string email)
        {
            this.email = email;
            columns = new Column[3];
            Column backlog = new Column("backlog", 0);
            Column in_progress = new Column("in progress", 1);
            Column done = new Column("done", 2);
            columns[0] = backlog;
            columns[1] = in_progress;
            columns[2] = done;
            taskId = 0;
            
        }

        public Board(string email, int taskID, int sizeOfColumnsArray)
        {
            this.email = email;
            columns = new Column[3];
            Column backlog = new Column("backlog", 0);
            Column in_progress = new Column("in progress", 1);
            Column done = new Column("done", 2);
            columns[0] = backlog;
            columns[1] = in_progress;
            columns[2] = done;
            taskId = 0;

        }

        public Board()
        {
        }

        public DALObject<Board> Import(string email)
        {
            return DALObject<Board>.FromJson(DALController.Read
                (Path.Combine("Boards",this.GetSafeFilename(email) + ".json")));
        }
        public override void Save()
        {
            DALController.Write(Path.Combine("Boards", this.GetSafeFilename(email)+ ".json"), this.ToJson());

        }
        public int getTaskID()
        {
            return this.taskId;
        }

        public Column[] GetColumns()
        {
            return this.columns;
        }

        public override string ToString()
        { return ("Board name: "+email+"\n"+ToJson()); }

        public string getEmail()
        {
            return this.email;
        }
    }
}