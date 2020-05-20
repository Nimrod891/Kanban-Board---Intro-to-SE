using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    class TaskDTO : DTO
    {
        public const string MessageTitleColumnName = "Title";
        public const string MessagedescriptionColumnName = "Description";
        public const string MessagecolumnColumnName = "Column";
        public const string MessageDueDateColumnName = "DueDate";
        public const string MessageCreationTimeColumnName = "CreationTime";

        // private string _taskid;
        // public string TaskID { get => _taskid; set { _taskid = value; _controller.Update(Id, MessageTaskIDColumnName, value); } }
        private string _title;
        public string Title { get => _title; set { _title = value; _controller.Update(Id, MessageTitleColumnName, value); } }
        private string _description;
        public string Description { get => _description; set { _description = value; _controller.Update(Id, MessagedescriptionColumnName, value); } }
        private long _coulumnId;
        public long ColumnId { get => _coulumnId; set { _coulumnId = value; _controller.Update(Id, MessagecolumnColumnName, value); } }



        private DateTime _duedate;
        public DateTime DueDate { get => _duedate; set { _duedate = value; _controller.Update(Id, MessageDueDateColumnName, value); } }
        private DateTime _creationtime;
        public DateTime CreationTime { get => _creationtime; set { _creationtime = value; _controller.Update(Id, MessageCreationTimeColumnName, value); } }



        public TaskDTO(long ID, string Title, string Description, DateTime DueDate, DateTime CreationTime, long columnID, string Email) : base(new TaskDalController())
        {
            email = Email;
            //Id = ID;
            _title = Title;
            _description = Description;
            _coulumnId = columnID;
            _duedate = DueDate;
            _creationtime = CreationTime;


        }



    }
}
