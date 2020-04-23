using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Objects
{
    public class Task : DALObject<Task>
    {
        public int taskId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime creationDate { get; set; }

        public Task(int taskId, string title, string description, DateTime dueDate, DateTime creationDate)
        {
            this.taskId = taskId;
            this.title = title;
            this.description = description;
            this.dueDate = dueDate;
            this.creationDate = creationDate;
        }

        public Task() { }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}
