using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    class Board : IPresistObject<DataAccessLayer.Objects.Board>
    {
        private string userEmail;
        private Dictionary<int, Column> columns;
        private int taskId;
        private int columnId;
        private bool is_UserLoggedin;

        public Board(string userEmail)
        {
            columnId = 0;
            this.userEmail = userEmail;
            Column backlog = new Column("backlog", columnId);
            columnId++;
            Column in_progress = new Column("in progress", columnId);
            columnId++;
            Column done = new Column("done", columnId);
            columnId++;
            columns.Add(backlog.GetColumnId(), backlog);
            columns.Add(in_progress.GetColumnId(), in_progress);
            columns.Add(done.GetColumnId(), done);
            taskId = 0;
            is_UserLoggedin = false;

        }
       
        public Board(DataAccessLayer.Objects.Board myBoard)
        {
            this.userEmail = myBoard.getEmail();
            this.taskId = myBoard.getTaskID();
            this.columns = new Dictionary<int, Column>();
           
            for(int i=0; i < myBoard.columns.Length; i++)
            {
                this.columns[i] = new Column(myBoard.columns[i]);
            }
            this.SetIsULoggedIn(true);
            

            //this.myBoard = new BoardPackage.Board(dalUser.myBoard);

        }

        public string GetUserEmail()
        {
            return userEmail;
        }

        public void SetIsULoggedIn(bool a)
        {
            this.is_UserLoggedin = a;
        }
        
        public Task AddNewTask(string title, string description, DateTime dueDate)
        {
            if (!is_UserLoggedin)/// getting exception here because is_UserLoggedin from this board
                                ///is not changed to true when the user is logged in.
            {
                throw new Exception("User is not logged in");
            }
            Task t;
            t = columns[0].AddTask(taskId,title, description, dueDate);
            taskId++;
            return t;
        }

        public void LimitTasks(int columnId, int limitNum)
        {
            if (!is_UserLoggedin)
            {
                throw new Exception("User is not logged in");
            }
            columns[columnId].SetLimitNum(limitNum);
        }

        public void AdvanceTask(int currentColId, int taskId)
        {
            if (!is_UserLoggedin)
            {
                throw new Exception("User is not logged in");
            }
            if (currentColId == columns.Count) // if you're in the last column
            {
                throw new Exception("You can't advance tasks from the last column");
            }
            if(currentColId < 0 || currentColId > columns.Count)
            {
                throw new Exception("Invalid colomn Ordinal");
            }
            columns[currentColId + 1].AddTasksToDict(taskId, columns[currentColId].GetTaskById(taskId)); // add task to the next column
            columns[currentColId].DeleteTask(taskId); // delete task from current column
        }

        public void UpdateTaskDueDate(int colId, int taskId, DateTime dueDate)
        {
            if (is_UserLoggedin)
                columns[colId].GetTaskById(taskId).SetDueDate(dueDate);
            else
                throw new Exception("User is not logged in");
        }
        public void UpdateTaskTitle(int colId, int taskId, string title)
        {
            if (is_UserLoggedin)
                columns[colId].GetTaskById(taskId).SetTitle(title);
            else
                throw new Exception("User is not logged in");
        }

        public void UpdateTaskDescription(int colId, int taskId, string description)
        {
            if (is_UserLoggedin)
                columns[colId].GetTaskById(taskId).SetDescription(description);
            else
                throw new Exception("User is not logged in");
        }

        public Column GetColumnById(int columnOrdinal)
        {
            if (columnOrdinal < 0 || columnOrdinal > columns.Count)
            {
                throw new Exception("Invalid column ordinal");
            }
            return columns[columnOrdinal];
        }

        public Column GetColumnByName(string colName)
        {
            int colId = -1;
            for (int i = 0; i < columns.Count; i++)
            {
                if (columns[i].GetName().Equals(colName))
                {
                    colId = columns[i].GetColumnId();
                }
            }
            if (colId == -1)
            {
                throw new Exception("there's no column named " + colName);
            }
            return columns[colId];
        }
        public DataAccessLayer.Objects.Board ToDalObject()
        {
            DataAccessLayer.Objects.Board dalBoard = new DataAccessLayer.Objects.Board(); // empty dal board
            /* fill with email, columnspublic string email { get; set; }
             public Column[] columns { get; set; }
              private int taskId { get; set; }*/
            dalBoard.email = this.userEmail;
            dalBoard.taskId = this.taskId;
            dalBoard.columns = new DataAccessLayer.Objects.Column[this.columns.Count];



            int columnsSize=this.columns.Count;
            for(int i=0; i<this.columns.Count;i++)
            {
                dalBoard.columns[i] = this.columns[i].ToDalObject();
            }

            return dalBoard;
            
        }
        public List<string> GetMyColumns()
        {
            List<string> columnsName = new List<string>();
            foreach(var c in columns)
            {
                columnsName.Add(c.Value.GetName());
            }
            return columnsName;
        }

    }
}
