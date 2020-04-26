﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    class Task : IPresistObject<DataAccessLayer.Objects.Task>
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
            SetDueDate(dueDate);
            this.creationDate = DateTime.Now;
        }

        public Task(DataAccessLayer.Objects.Task dalTask)
        {
            this.taskId = dalTask.taskId;
            this.title = dalTask.title;
            this.description = dalTask.description;
            this.dueDate = dalTask.dueDate;
            this.creationDate = dalTask.creationDate;
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
            if (title.Length > 50)
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
            if (description.Length > 300)
            {
                throw new Exception("description is over 300 chars");
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

        public DataAccessLayer.Objects.Task ToDalObject()
        {
            DataAccessLayer.Objects.Task dalTask = new DataAccessLayer.Objects.Task(this.taskId,this.title, this.description, this.dueDate,this.creationDate);
            return dalTask;
        }
    }
}
