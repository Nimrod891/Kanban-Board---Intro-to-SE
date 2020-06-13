using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using IntroSE.Kanban.Backend.ServiceLayer;
using Task = IntroSE.Kanban.Backend.ServiceLayer.Task;

namespace WpfApp1.Model
{
    class ColumnModel : NotifableModelObject
    {
        private string _name;
        private int _limitNum;
        //private int _numOfTasks;
        private int _columnordianl;
       // private readonly UserModel user;
        public ObservableCollection<TaskModel> _tasks;
        public ColumnModel(BackendController controller, string email, int columnordinal, string name, int limit, IReadOnlyCollection<Task> tasks) : base(controller)
        {
            UserEmail = email;
            _columnordianl = columnordinal;
            _name = name;
            _limitNum = limit;
            _tasks = new ObservableCollection<TaskModel>(
                controller.GetColumn(email, columnordinal).Value.Tasks.Select((c, i) => new TaskModel(controller, tasks.ToList<Task>()[i].Id,  tasks.ToList<Task>()[i].Title, tasks.ToList<Task>()[i].DueDate, tasks.ToList<Task>()[i].CreationTime, tasks.ToList<Task>()[i].Description, email,columnordinal)));


        }
        public int LimitNum {
            get => _limitNum;
            set
            {
                this._limitNum = value;
                RaisePropertyChanged("LimitNum");
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                this._title = value;
                RaisePropertyChanged("Title");
            }
        }
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                this._description = value;
                RaisePropertyChanged("Description");
               // Controller.UpdateTaskDescription(UserEmail, _columnordianl, _taskid, _description);
            }
        }
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

        private DateTime _creationtime;
        public DateTime CreationTime
        {
            get => _creationtime;
            set {
                _creationtime = value;

            }
        }
        private DateTime _dueDate;
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

        private string UserEmail; //storing this user here is an hack becuase static & singletone are not allowed.
    }

}
