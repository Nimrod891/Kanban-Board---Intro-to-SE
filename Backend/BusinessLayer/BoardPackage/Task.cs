using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    class Task
    {
       
        private int taskId;
        private string title;
        private string description;
        private DateTime dueDate;
        private DateTime creationDate;

        public Task(int taskId, string title, string description, DateTime dueDate)
        {

            this.taskId = taskId;
            SetTitle(title);
            SetDescription(description);
            this.dueDate = dueDate;
            this.creationDate = DateTime.Now;
        }

        public int GetTaskId()
        {
            return taskId;
        }

        public string GetTitle()
        {
            return title;
        }

        public string GetDescription()
        {
            return description;
        }

        public DateTime GetDueDate()
        {
            return dueDate;
        }

        public DateTime GetCreationDate()
        {
            return creationDate;
        }

       
        public void SetTitle(string title)
        {
            if (!(title.Length > 50))
            {
                this.title = title;
            }
            else
            {
                throw new Exception("Title is over 50 chars");
            }
            if (!(String.IsNullOrWhiteSpace(title)))
            {
                this.title = title;
            }
            else
            {
                throw new Exception("Title is empty");
            }
        }

        public void SetDescription(string description)
        {
            if (!(description.Length > 300))
            {
                this.description = description;
            }
            else
            {
                throw new Exception("description is over 300 chars");
            }
        }

        public void SetDueDate(DateTime dueDate)
        {
            if(dueDate < DateTime.Now)
            {
                throw new Exception("This date has already passed");
            }
            this.dueDate = dueDate;
        }

    }

}
