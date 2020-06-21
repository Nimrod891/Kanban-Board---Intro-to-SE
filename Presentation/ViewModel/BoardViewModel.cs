using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;
using Presentation.Model;
using System.ComponentModel;
using System.Windows;
using Presentation.View;
using System.Collections.ObjectModel;

namespace Presentation.ViewModel
{
    class BoardViewModel : NotifableObject
    {
        public BackendController Controller { get; private set; }
        private string _username;
        private BoardModel myBoard;
        private ObservableCollection<ColumnModel> _columns;
        private ObservableCollection<string> _column_names;
        private readonly string _emailCreator;
        //private ObservableCollection<ObservableCollection<TaskModel>> tasks;
        private ObservableCollection<ObservableCollection<TaskModel>> _tasks;


        public BoardViewModel(BackendController controller, string email) {
            this.Controller = controller;
            this._username = email;
            myBoard = new BoardModel(controller, email);
            this._emailCreator = myBoard.getUser().getUseremail();
            this._column_names = myBoard.GetColumnsNames();
            _columns = myBoard.Columns;
            ObservableCollection<ObservableCollection<TaskModel>> t = new ObservableCollection<ObservableCollection<TaskModel>>();
            for (int i = 0; i < _columns.Count; i++)
            {
                try
                {
                    if (_columns[i]._tasks.Count != 0)
                    {
                        ColumnModel col = new ColumnModel(controller, email, i, controller.GetColumn(email, i).Value.Name, controller.GetColumn(email, i).Value.Limit, controller.GetColumn(email, i).Value.Tasks);
                        t.Add(col.Task);
                    }


                } catch (Exception)
                {
                    continue;
                }
            }
            Tasks = t;
        }
        private ColumnModel selectedcolumn;
        public ColumnModel SelectedColumn
        {get => selectedcolumn;
            set
            {
                selectedcolumn = value;
                RaisePropertyChanged("SelectedColumn");


            }

        }
        public ObservableCollection<ObservableCollection<TaskModel>> Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
                RaisePropertyChanged("Tasks");
            }
        }


        public ObservableCollection<ColumnModel> Columns
        {
            get => _columns;
            private set
            {
                _columns = value;
                RaisePropertyChanged("Columns");
            }
        }
        public ObservableCollection<string> ColumnNames
        {
            get => this._column_names;
            private set
            {
                _column_names = value;
                RaisePropertyChanged("Columns");
            }
        }
        public Model.BoardModel MyBoard { get { return myBoard; } }
        public string Username
        {
            get => _username;
            set
            {
                //this._username = value;
                //RaisePropertyChanged("Username");
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
        private string newname = "";
        public string NewName
        {
            get { return newname; }
            set
            {
                newname = value;
                RaisePropertyChanged("NewName");
            }
        }
        private string limitnum ;
        public string NewLimitNum
        {
            get { return limitnum; }
            set
            {
                limitnum = value;
                RaisePropertyChanged("NewLimitNum");
            }
        }
        private string newtasktitle="";
        public string NewTaskTitle
        {
            get { return newtasktitle; }
            set
            {
                newtasktitle = value;
                RaisePropertyChanged("NewTaskTitle");
            }
        }
        private string newtaskdescription = "";
        public string NewTaskDescription
        {
            get { return newtaskdescription; }
            set
            {
                newtaskdescription = value;
                RaisePropertyChanged("NewTaskDescription");
            }
        }
        private DateTime new_taskd_due_date=DateTime.Now ;
        public DateTime NewTaskDueDate
        {
            get { return new_taskd_due_date; }
            set
            {
                new_taskd_due_date = value;
                RaisePropertyChanged("NewTaskDueDate");
            }
        }
        private int _colOrdinal;
        public int ColOrdinal
        {
            get { return _colOrdinal; }
            set
            {
                _colOrdinal = value;
                RaisePropertyChanged("ColOrdinal");
            }
        }
        private string _new_col_name;
        public string NewColName
        {
            get { return _new_col_name; }
            set
            {
                _new_col_name = value;
                RaisePropertyChanged("ColOrdinal");
            }
        }


        public void GetColumn()
        {
            try
            {
                Controller.GetColumn(Username, SelectedColumn.NameColumn);
                var Column = new ColumnWindow(Controller.Service, Username, SelectedColumn.ColumnOrdianl);
                Column.Show();
                
                
            }
            catch (Exception )
            {
                MessageBox.Show("Please select one of the columns in the list before clicking this botton.");
            }
            
        }

        public void LimitColumnTask(int column_id, int limit)
        {
            Message = "";
            try
            {
                Controller.LimitColumnTasks(_username, column_id, limit);
                MessageBox.Show( $"limit column {column_id} successfully");
            }
            catch (Exception e)
            {
                MessageBox.Show( e.Message);
            }
           
        }

        /*
                public BoardViewModel(BackendController controller, string user)
                {
                    this.Controller = controller;
                    this.Username = user;
                    this.myBoard = controller.GetBoard(user);
                    RaisePropertyChanged("BoardTitle");
                }
                */
        public string BoardTitle
        {
            get { return $"Hello  {_username} "; }
        }
        
        public void AddTask()
        {
            try
            {
                TaskModel res = this.Controller.AddTask(Username, NewTaskTitle, NewTaskDescription,NewTaskDueDate,0);
                NewTaskTitle = "";
                newtaskdescription = "";
                NewTaskDueDate = DateTime.Now;
                MessageBox.Show("Task " + res.Taskid + " was added successfully to your " + _columns.First());
                myBoard = new BoardModel(this.Controller, Username);
                for (int i = 0; i < _columns.Count; i++)
                {
                    try
                    {
                        if (_columns[i]._tasks.Count != 0)
                            this._tasks.Add(_columns[i].Task);


                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        public void LogOut(string email)
        {
            try
            {
                Controller.Logout(Username);
            }catch(Exception )
            {
                MessageBox.Show("log out failed");
            }
        }
        public void SetLimitNum(string email, int columnOrdinal, string limit)
        {
            int k = 0;
            if (!int.TryParse(limit, out k))
            {
                MessageBox.Show("Limit must be an integer.");
                NewLimitNum = "";
                return;
            }
            try
            {
                Controller.LimitColumnTasks(email, columnOrdinal, k);
                NewLimitNum = "";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            if (k == -1)
                MessageBox.Show($"The limit of your { _column_names.ElementAt<string>(columnOrdinal)} column was disabled");
            else
                MessageBox.Show($"The limit of your  { _column_names.ElementAt<string>(columnOrdinal) } column was set to {k}");
            myBoard = new BoardModel(this.Controller, Username);
            ColumnNames = myBoard.GetColumnsNames();
            Columns = myBoard.Columns;

        }

        public void AddColumn()
        {
            Message = "";
            try
            {
                
                Controller.AddColumn(_username,ColOrdinal, NewColName);
                MessageBox.Show( $" column {NewColName} was added  successfully");
                myBoard = new BoardModel(this.Controller, Username);
                ColumnNames = myBoard.GetColumnsNames();
                Columns = myBoard.Columns;

            }
            catch (Exception e)
            {
                MessageBox.Show( e.Message);
            }
        }
        public void RemoveColumn(int col)
        {
            Message = "";
            try
            {
                Controller.RemoveColumn(_username, col);
                MessageBox.Show($" column {col} was removed  successfully");
                myBoard = new BoardModel(this.Controller, Username);
                ColumnNames = myBoard.GetColumnsNames();
                Columns = myBoard.Columns;

            }
            catch (Exception e)

            {
                MessageBox.Show( e.Message);
            }
        }
        public void MoveColumnLeft (int col)
        {
            Message = "";
            try
            {
                Controller.MoveColumnLeft(_username,col);
                Message = $" column {col} was moved Left successfully";
                myBoard = new BoardModel(this.Controller, Username);
                ColumnNames = myBoard.GetColumnsNames();
                Columns = myBoard.Columns;
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }
        public void MoveColumRight(int col)
        {
            Message = "";
            try
            {
                Controller.MoveColumnRight(_username, col);
                Message = $" column {col} was moved right successfully";
                myBoard = new BoardModel(this.Controller, Username);
                ColumnNames = myBoard.GetColumnsNames();
                Columns = myBoard.Columns;
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }

    }
}
