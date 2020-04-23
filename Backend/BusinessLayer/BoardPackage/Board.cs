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
        private Column[] columns;
        private int taskId;
        private bool is_UserLoggedin;

        public Board(string userEmail)
        {
            this.userEmail = userEmail;
            columns = new Column[3];
            Column backLog = new Column("BackLog", 0);
            Column inProgress = new Column("InProgress", 1);
            Column done = new Column("Done", 2);
            columns[0] = backLog;
            columns[1] = inProgress;
            columns[2] = done;
            taskId = 0;
            is_UserLoggedin = false;


        }
        public Board(DataAccessLayer.Objects.Board myBoard)
        {
            this.userEmail = myBoard.getEmail();
            this.taskId = myBoard.getTaskID();
            this.columns = new Column[myBoard.columns.Length];
            //foreach(DataAccessLayer.Objects.Column newColumn in myBoard.columns)
            //{
            //    this.columns[]
            //}
            for(int i=0; i < myBoard.columns.Length; i++)
            {
                this.columns[i] = new Column(myBoard.columns[i]);
            }
            

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
        public void AddNewTask(string title, string description, DateTime dueDate)
        {
            if (!is_UserLoggedin)
            {
                throw new Exception("User is not logged in");
            }
            taskId++;
            columns[0].AddTask(taskId, title, description, dueDate);
        }

        public void LimitTasks(int columnId, int limitNum)
        {
            if (!is_UserLoggedin)
            {
                throw new Exception("User is not logged in");
            }
            if (columnId != 1)
            {
                throw new Exception("You can only limit the number of tasks in "  + columns[1].GetName() + " column");
            }
                columns[columnId].SetLimitNum(limitNum);
        }

        public void AdvanceTask(int currentColId, int taskId)
        {
            if (is_UserLoggedin)
            {
                throw new Exception("User is not logged in");
            }
            if (currentColId == columns.Length - 1) // if you're in the last column
            {
                throw new Exception("You can't advance tasks from the last column");
            }
            if(currentColId < 0 || currentColId > columns.Length)
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
            if (columnOrdinal < 0 || columnOrdinal > columns.Length)
            {
                throw new Exception("Invalid column ordinal");
            }
            return columns[columnOrdinal];
        }

        public Column GetColumnByName(string colName)
        {
            int colId = -1;
            for (int i = 0; i < columns.Length - 1; i++)
            {
                if (colId != -1)
                {
                    colId = columns[i].GetColumnIdByName(colName);
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
            DataAccessLayer.Objects.Board dalBoard = new DataAccessLayer.Objects.Board();
            return dalColumn;
        }
    }
}
