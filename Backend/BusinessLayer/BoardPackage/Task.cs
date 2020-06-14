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
        private int maxDesc = 300;
        private int maxTitle = 50;
        private string taskAssignee;

        public Task(int taskId, string title, string description, DateTime dueDate)
        {
            this.taskId = taskId;
            SetTitle(title);
            SetDescription(description);
            SetDueDate(dueDate);
            this.creationDate = DateTime.Now;
        }
        public Task(int taskId, string title, string description, DateTime dueDate, DateTime creationDate)
        {
            this.taskId = taskId;
            this.title = title;
            this.description = description;
            this.dueDate = dueDate;
            this.creationDate = creationDate;
        }
        public string getTaskAssignee()
        {
            return this.taskAssignee;
        }
        public void setTaskAssignee(string taskAssignee)
        {
            this.taskAssignee = taskAssignee;
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
            if (title.Length > maxTitle)
            {
                throw new Exception("Title is over 50 chars");
            }
            if (String.IsNullOrWhiteSpace(title))
            {
                throw new Exception("Title can't be empty");
            }
            this.title = title;
        }

        public void SetDescription(string description)
        {
            if (description != null || !String.IsNullOrWhiteSpace(description))
            {
                if (description.Length > maxDesc)
                {
                    throw new Exception("description is over 300 chars");
                }
            }

            this.description = description;
        }

        public void SetDueDate(DateTime dueDate)
        {
            if (dueDate < DateTime.Now)
            {
                throw new Exception("This date has already passed");
            }
            this.dueDate = dueDate;
        }


    }

}
