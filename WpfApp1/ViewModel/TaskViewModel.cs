using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    class TaskViewModel : NotifableObject
    {
        private string _username;
        private int _id;
        private DateTime _creationTime;
        private DateTime _dueDate;
        private string _title;
        private string _description;
        private BackendController _TaskController { get;  set; }
        private int _columnId;

        public TaskViewModel(BackendController BackendController,string user,int col, IntroSE.Kanban.Backend.ServiceLayer.Task t )
        {
            _username = user;
            TaskId = t.Id;
            CreationTime = t.CreationTime;
            DueDate = t.DueDate;
            Description = t.Description;
            _columnId = col;
            this._username = user;
            _TaskController = BackendController;
        }


        
        public string Description
        {
            get => _description;
            set
            {
                this._description = value;
                RaisePropertyChanged("Description");
            }
        }
        public int TaskId
        {
            get => _id;
            set
            {
                this._id = value;
                RaisePropertyChanged("TaskId");
            }
        }
        public DateTime CreationTime
        {
            get => _creationTime;
            set
            {
                _creationTime = CreationTime;
            }
        }
        public DateTime DueDate
        {
            get => _dueDate;
            set
            {
                this._dueDate = value;
                RaisePropertyChanged("DueDate");
            }
        }
        public string TaskTitle {
            get => this._title;
            set
            {
                this._title = value;
                RaisePropertyChanged("TaskTitle");
            }
                } 
        private string _message;

        public string Message
        {
            get => _message;
            set
            {
                this._message = value;
                RaisePropertyChanged("Message");
            }
        }
        public void AddTask()
        {
            Message = "";
            try
            {
                _TaskController.AddTask(_username, _title, _description, _dueDate,_columnId);
                Message = "Registered successfully";

            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }
        public void UpdateTaskDiscription(string Description)
        {
            Message = "";
            try
            {
                 _TaskController.UpdateTaskDescription(_username, this._columnId, _id, Description);
                Message = "Task Discription successfully";

            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }
        public void UpdateTaskDueDate(DateTime newDate)
        {
            Message = "";
            try
            {
                
                _TaskController.UpdateTaskDueDate(_username, _columnId, _id, newDate);
                Message = " Due Date Changed successfully";

            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }
        public void UpdateTaskTitle()
        {
            Message = "";
            try
            {

                _TaskController.UpdateTaskTitle(_username, _columnId, _id, TaskTitle);
                Message = " Due Date Changed successfully";

            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }
    }
}
