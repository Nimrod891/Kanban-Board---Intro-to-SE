using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Objects
{ }
/*
    //[JsonObject(MemberSerialization.Fields)]
    public class Board : DALObject<Board>
    {
        public string email { get; set; }
        public Dictionary<int,Column> columns { get; set; }
        public int taskId { get; set; }
        public int columnId;

        public Board(string email)
        {
            this.email = email;
            columns = new Dictionary<int, Column>();
            columnId = 0;
            Column backlog = new Column("backlog", columnId);
            columnId++;
            Column in_progress = new Column("in progress", columnId);
            columnId++;
            Column done = new Column("done", columnId);
            columns.Add(backlog.columnId, backlog);
            columns.Add(in_progress.columnId, in_progress);
            columns.Add(done.columnId, done);
            taskId = 0;
            
        }

        public Board(string email, int taskID, int sizeOfColumnsArray)
        {
            this.email = email;
            //columns = new Column[3];
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
            return DALObject<Board>.FromJson(DalController.Read
                (Path.Combine("Boards",this.GetSafeFilename(email) + ".json")));
        }
        public override void Save()
        {
            DalController.Write(Path.Combine("Boards", this.GetSafeFilename(email)+ ".json"), this.ToJson());

        }
        public int getTaskID()
        {
            return this.taskId;
        }
        public Dictionary<int,Column> GetColumns()
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
*/