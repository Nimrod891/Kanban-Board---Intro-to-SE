using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            _creationTime = t.CreationTime;
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
                RaisePropertyChanged("CreationTime");
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
        /*
        public Brush Date75
        {
            get
            {
                
                double now = (double)DateTime.Now.Year + (double)DateTime.Now.Month / 12 + (double)DateTime.Now.Day / 365 / 12 + (double)DateTime.Now.Hour / 365 / 12 / 24 + (double)DateTime.Now.Minute / 365 / 12 / 24 / 60 + (double)DateTime.Now.Second / 365 / 12 / 24 / 60 / 60;
                double create = (double)_creationTime.Year -1+ (double)_creationTime.Month / 12 + (double)_creationTime.Day / 365 / 12 + (double)_creationTime.Hour / 365 / 12 / 24 + (double)_creationTime.Minute / 365 / 12 / 24 / 60 + (double)_creationTime.Second / 365 / 12 / 24 / 60 / 60;
                double dues = (double)_dueDate.Year + (double)_dueDate.Month / 12 + (double)_dueDate.Day / 365 / 12 + (double)_dueDate.Hour / 365 / 12 / 24 + (double)_dueDate.Minute / 365 / 12 / 24 / 60 + (double)_dueDate.Second / 365 / 12 / 24 / 60 / 60;
                if ((now - create) / (dues - now) >= 0.75)
                {
                    return Brushes.Orange;
                }
                return Brushes.Red;
            }
        }

        /*
        public Brush Date75()
        {
                double now = DateTime.Now.Year + DateTime.Now.Month / 12 + DateTime.Now.Day / 365 / 12 + DateTime.Now.Hour / 365 / 12 / 24 + DateTime.Now.Minute / 365 / 12 / 24 / 60 + DateTime.Now.Second / 365 / 12 / 24 / 60 / 60;
                double create = _creationTime.Year + _creationTime.Month / 12 + _creationTime.Day / 365 / 12 + _creationTime.Hour / 365 / 12 / 24 + _creationTime.Minute / 365 / 12 / 24 / 60 + _creationTime.Second / 365 / 12 / 24 / 60 / 60;
                double dues = _dueDate.Year + _dueDate.Month / 12 + _dueDate.Day / 365 / 12 + _dueDate.Hour / 365 / 12 / 24 + _dueDate.Minute / 365 / 12 / 24 / 60 + _dueDate.Second / 365 / 12 / 24 / 60 / 60;
                if ((now - create) / (dues - now) > 0.75)
                {
                return Brushes.Orange;
                }
                
            return Brushes.Red;
            }
            */
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
                MessageBox.Show( e.Message);
            }
        }
        public void UpdateTaskDueDate(DateTime newDate)
        {
            try
            {
                
                _TaskController.UpdateTaskDueDate(_username, _columnId, _id, newDate);
                Message = " Due Date Changed successfully";

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void UpdateTaskTitle()
        {
            try
            {

                _TaskController.UpdateTaskTitle(_username, _columnId, _id, TaskTitle);
                Message = " Due Date Changed successfully";

            }
            catch (Exception e)
            {
                 MessageBox.Show( e.Message);
            }
        }
    }
}
