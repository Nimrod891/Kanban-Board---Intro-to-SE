using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace WpfApp1.Model
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

