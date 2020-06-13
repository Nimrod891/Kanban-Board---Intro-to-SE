using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Model;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.ComponentModel;

namespace WpfApp1.ViewModel
{
    class ColumnViewModel : NotifableModelObject
    {
        //public BackendController Controller { get; private set; }
        private string _username;
        private int _columnorinal;
        private ObservableCollection<TaskModel> _tasks;
        private int _limit;


        public ColumnViewModel(BackendController controller, string email, int columOrdinal): base(controller)
        {
            ColumnModel col = new ColumnModel(controller, email, columOrdinal, controller.GetColumn(email, columOrdinal).Value.Name, controller.GetColumn(email, columOrdinal).Value.Limit, controller.GetColumn(email, columOrdinal).Value.Tasks);
            this._username = email;
            _columnorinal = columOrdinal;
            this._tasks = col._tasks;
            this._limit = col.LimitNum;




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
        /*
        public void DeleteTask()
        {
            Message = "";
            try
            {
                Controller.DelteTask(Username, SelectedTask.ColumnOrdianl,SelectedTask.Taskid);
                MessageBox.Show($" task {SelectedTask.Title} was removed  successfully");
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            ColumnModel col = new ColumnModel(Controller, _username, _columnorinal, Controller.GetColumn(_username,_columnorinal).Value.Name, Controller.GetColumn(_username, _columnorinal).Value.Limit, Controller.GetColumn(_username, ColumnOrdianl).Value.Tasks);
            _tasks = col._tasks;

        }
        */

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
               Controller.AdvanceTask(Username, _columnorinal,SelectedTask.Taskid);
            }
            catch (Exception e)
            {
                Message = e.Message;
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
                Message = e.Message;
            }
        }
        public void GetTask()
        {
            Message = "";
            try
            {
                Controller.GetTask(Username, _columnorinal, SelectedTask.Taskid);

                
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }




    }
}
