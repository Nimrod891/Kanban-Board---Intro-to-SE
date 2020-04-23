using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Objects
{
    class Task : DALObject<Task>
    {
        private int taskId;
        private string title;
        private string description;
        private DateTime dueDate;
        private DateTime creationDate;

        public Task(int taskId, string title, string description, DateTime dueDate, DateTime creationDate)
        {
            this.taskId = taskId;
            this.title = title;
            this.description = description;
            this.dueDate = dueDate;
            this.creationDate = creationDate;
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}
