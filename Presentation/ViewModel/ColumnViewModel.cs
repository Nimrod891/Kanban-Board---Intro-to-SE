using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Presentation.Model;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.ComponentModel;
using System.Windows.Media;
using Presentation.View;

namespace Presentation.ViewModel
{
    class ColumnViewModel : NotifableModelObject
    {
        //public BackendController Controller { get; private set; }
        private string _username;
        private int _columnorinal;
        private ObservableCollection<TaskModel> _tasks;
        private int _limit;
        private string _titlecolumn="";
        private string _filter = "";


        public ColumnViewModel(BackendController controller, string email, int columOrdinal) : base(controller)
        {
            ColumnModel col = new ColumnModel(controller, email, columOrdinal, controller.GetColumn(email, columOrdinal).Value.Name, controller.GetColumn(email, columOrdinal).Value.Limit, controller.GetColumn(email, columOrdinal).Value.Tasks);
            this._username = email;
            _columnorinal = columOrdinal;
            this._tasks = col._tasks;
            this._limit = col.LimitNum;
            TitleColumn = col.NameColumn;




        }

        public ObservableCollection<TaskModel> TasksofColumns
        {
            get => _tasks;
            set
            {
                _tasks = value;
                RaisePropertyChanged("TasksofColumns");
            }
        }

        public ObservableCollection<TaskModel> TaskOfColumn
        {
            get => _tasks;
            private set
            {
                _tasks = value;
                RaisePropertyChanged("TaskOfColumn");
            }
        }
        public string TitleColumn
        {
            get => _titlecolumn;
            set
            {
                this._titlecolumn = value;
                RaisePropertyChanged("TitleColumn");
            }
        }
        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                RaisePropertyChanged("Filter");
            }
        }
        public string Username
        {
            get => _username;
            set
            {
                //this._username = value;
                //RaisePropertyChanged("Username");
            }
        }
        private string _assignemail;
        public string AssignEmail 
        {
            get => _assignemail;
            set
            {
                this._assignemail = value;
                RaisePropertyChanged("AssignEmail");
            }
        }
        public int ColumnOrdianl
        {
            get => _columnorinal;
            set
            {
                this._columnorinal = value;
                RaisePropertyChanged("ColumnOrdianl");
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


        private TaskModel _selected_task;
        public TaskModel SelectedTask
        {
            get => _selected_task;
            set {
                _selected_task = value;
                RaisePropertyChanged("SelectedTask");
            }
        }
        public void AdvanceTask()
        {
            Message = "";
            try
            {
                Controller.AdvanceTask(Username, ColumnOrdianl, SelectedTask.Taskid);
                ColumnModel col = new ColumnModel(this.Controller, this.Username, _columnorinal, Controller.GetColumn(Username, _columnorinal).Value.Name, Controller.GetColumn(_username, _columnorinal).Value.Limit, Controller.GetColumn(_username, _columnorinal).Value.Tasks);
                TaskOfColumn = col.Task;


            }
            catch (Exception e)
            {
                if (SelectedTask != null)
                    MessageBox.Show(e.Message);
                else
                {
                    MessageBox.Show("Choose task");
                }
            }
        }
        public void ChangeColName()
        {
            Message = "";
            try
            {
                Controller.ChangeTitle(Username, ColumnOrdianl, TitleColumn);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void AsignTask()
        {
            Message = "";
            try
            {
                Controller.AssignTask(Username, ColumnOrdianl, SelectedTask.Taskid, AssignEmail);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void BlueAssign()
        {
            Message = "";
            try
            {
                Controller.AssignTask(Username, ColumnOrdianl, SelectedTask.Taskid, AssignEmail);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void FilterTasks()
        {
            try
            {
                ObservableCollection<TaskModel> t = new ObservableCollection<TaskModel>();
                for (int i = 0; i < _tasks.Count; i++)
                {
                    if (_tasks[i].Description.Contains(Filter) || _tasks[i].Title.Contains(Filter))
                        t.Add(_tasks[i]);
                }
                TaskOfColumn = t;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void AddTask()
        {
            Message = "";
            try
            {
                Controller.AdvanceTask(Username, _columnorinal, SelectedTask.Taskid);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void SortByDate()
        {
            if (_tasks.Count == 1)
                return;
            ObservableCollection<TaskModel> temp = new ObservableCollection<TaskModel>();
            int k = _tasks.Count;
            for (int j = 0; j < k; j++) {

                TaskModel t = null;
                if (_tasks.Count > 0)
                {
                    t = _tasks[0];
                    for (int i = 0; i < _tasks.Count; i++)
                    {
                        if (DateTime.Compare(t.Duedate, _tasks[i].Duedate) > 0 )
                        {
                            t = _tasks[i];
                        }

                    }
                    if (t == null)
                    {
                        break;
                    }
                    else
                    {
                        temp.Add(t);
                        _tasks.Remove(t);

                    }
                }
                else break;

            }
            //temp.Add(_tasks[0]);
            TaskOfColumn = temp;
        }
        public TaskModel GetTaski
        {
            get
            {
                GetTask();
                RaisePropertyChanged("GetTaski");
                return SelectedTask;

            }

        }
    public void GetTask()
        {
            //Message = "";
            try
            {
                var Taski = new TaskWindow(Username, this.Controller.Service, _columnorinal, SelectedTask.Taskid);
                Taski.Show();

            }
            catch (Exception e)
            {
                if (SelectedTask != null)
                    MessageBox.Show(e.Message);
                else
                {
                    MessageBox.Show("Choose task");
                }
            }
        }
        public void DeleteTask()
        {
            Message = "";
            try
            {
                Controller.DeleteTask(Username, _columnorinal, SelectedTask.Taskid);
                ColumnModel col = new ColumnModel(this.Controller, this.Username, _columnorinal, Controller.GetColumn(Username, _columnorinal).Value.Name, Controller.GetColumn(_username, _columnorinal).Value.Limit, Controller.GetColumn(_username, _columnorinal).Value.Tasks);
                TaskOfColumn = col.Task;



            }
            catch (Exception e)
            {
                if(SelectedTask!=null)
                    MessageBox.Show (e.Message);
                else
                {
                    MessageBox.Show("Choose task");
                }
            }
        }



    }
}
