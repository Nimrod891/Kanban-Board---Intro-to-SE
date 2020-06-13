using System;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public struct Task
    {
        public readonly int Id;
        public readonly DateTime CreationTime;
        public readonly DateTime DueDate;
        public readonly string Title;
        public readonly string Description;
        //public readonly string emailAsignee; // add to Task signature as well
        internal Task(int id, DateTime creationTime, DateTime dueDate, string title, string description)
        {
            this.Id = id;
            this.CreationTime = creationTime;
            this.DueDate = dueDate;
            this.Title = title;
            this.Description = description;
            //this.emailAssignee = emailAssignee;
        }
        // You can add code here
    }
}