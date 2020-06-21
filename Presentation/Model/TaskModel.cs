using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace Presentation.Model
{
    class TaskModel : NotifableModelObject
    {
        private int _taskid;
        private string _title;
        private string _description;
        private DateTime _dueDate;
        private DateTime _creationtime;
        private int _columnordianl;
        private string UserEmail;
        private string _emailAsiigni;


        public TaskModel(BackendController controller, int task_id, string title, DateTime duedate, DateTime creation_time, string description, string user_email,int col) : base(controller)
        {
            UserEmail = user_email;
            _columnordianl = col;
            _taskid = task_id;
            _title = title;
            _description = description;
            _dueDate = duedate;
            _creationtime = creation_time;
        }

        // public TaskModel(BackendController controller, IntroSE.Kanban.Backend.ServiceLayer.Task task, string email) : this(controller, task.Id, controller.GetColumn(em, task.Title, task.DueDate, task.CreationTime, task.Description, email) { }

        public Brush Date75
        {
            get
            {
                double now = (double)DateTime.Now.Year + (double)DateTime.Now.Month / 12 + (double)DateTime.Now.Day / 365 / 12 + (double)DateTime.Now.Hour / 365 / 12 / 24 + (double)DateTime.Now.Minute / 365 / 12 / 24 / 60 + (double)DateTime.Now.Second / 365 / 12 / 24 / 60 / 60;
                double create = (double)_creationtime.Year + (double)_creationtime.Month / 12 + (double)_creationtime.Day / 365 / 12 + (double)_creationtime.Hour / 365 / 12 / 24 + (double)_creationtime.Minute / 365 / 12 / 24 / 60 + (double)_creationtime.Second / 365 / 12 / 24 / 60 / 60;
                double dues = (double)_dueDate.Year + (double)_dueDate.Month / 12 + (double)_dueDate.Day / 365 / 12 + (double)_dueDate.Hour / 365 / 12 / 24 + (double)_dueDate.Minute / 365 / 12 / 24 / 60 + (double)_dueDate.Second / 365 / 12 / 24 / 60 / 60;
                if(DateTime.Compare(_dueDate,DateTime.Now)<0)
                {
                    return Brushes.Red;
                }
                if ((now - create) / (dues - now) >= 0.75)
                {
                    return Brushes.Orange;
                }
                return null;
            }
        }
        public Brush BlueAssigni
        {
            get
            {
                if (UserEmail.Equals(Controller.GetTask(UserEmail, _columnordianl, _taskid).Value.emailAssignee))
                    return Brushes.Blue;               
                return Brushes.Red;
            }
        }


        public int Taskid
        {
            get => _taskid;
            set
            {
                this._taskid = value;
                RaisePropertyChanged("Taskid");
            }
        }
        public string Title
        {
            get => _title;
            set
            {
                this._title = value;
                RaisePropertyChanged("Title");
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                this._description = value;
                RaisePropertyChanged("Description");
                //Controller.UpdateTaskDescription(UserEmail, _columnordianl,_taskid, _description);
            }
        }
        /*
        public int ColumnOrdianl
        {
            get => _columnordianl;
            set
            {
                this._columnordianl = value;
                RaisePropertyChanged("Columnoridanl");
               // Controller.(UserEmail, Id, value);
            }
        }
        */

        public DateTime CreationTime
        {
            get => _creationtime;
            set { }
        }
        public DateTime Duedate
        {
            get => _dueDate;
            set
            {
                this._dueDate = value;
                RaisePropertyChanged("Duedate");
                //Controller.UpdateTaskDueDate(UserEmail, _columnordianl, _taskid, value);
            }
        }
        

    }
}

