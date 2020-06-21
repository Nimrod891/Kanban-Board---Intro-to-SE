using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Tests")]
namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    class Board
    {
       
        private static int num_Boards;
        private int Id_board;
        private string creatorEmail;
        private Dictionary<int, Column> columns;
        private int taskId;
        private int columnId;
        private int minColumns = 2;
        private bool is_UserLoggedin;
        private string loggedInUser;
        private DataAccessLayer.BoardDalController myBoardDC;
        private DataAccessLayer.ColumnDalController myColumnDC;
        //private DataAccessLayer.TaskDalController myTaskDC;

        public Board(string userEmail)
        {
            columns = new Dictionary<int, Column>();
            this.creatorEmail = userEmail;
            columnId = 0;
            Column backlog = new Column("backlog", columnId);
            columnId++;
            Column in_progress = new Column("in progress", columnId);
            columnId++;
            Column done = new Column("done", columnId);
            columns.Add(backlog.GetColumnId(), backlog);
            columns.Add(in_progress.GetColumnId(), in_progress);
            columns.Add(done.GetColumnId(), done);
            taskId = 0;
            is_UserLoggedin = false;
        }
        public Board()// Testing Constructor
        {
            columns = new Dictionary<int, Column>();
            this.is_UserLoggedin = false;
            
        }

        public Board(string userEmail, string emailHost)
        {
            columns = new Dictionary<int, Column>();
            this.creatorEmail = emailHost;
            is_UserLoggedin = false;
        }

        public Board(int id,string userEmail)
        {
            columns = new Dictionary<int, Column>();
            this.creatorEmail = userEmail;

            //is_UserLoggedin = false;
        
        }

        public string GetUserEmail()
        {
            return creatorEmail;
        }

        public void SetIsULoggedIn(bool a)
        {
            this.is_UserLoggedin = a;
        }
        public Task AddNewTask(string title, string description, DateTime dueDate, string assigneeEmail)
        {
            if (!is_UserLoggedin)/// getting exception here because is_UserLoggedin from this board
                                ///is not changed to true when the user is logged in.
            {
                throw new Exception("User is not logged in");
            }
            Task a = columns[0].AddTask(taskId, title, description, dueDate, assigneeEmail);
            //a.setEmailAssignee(this.loggedInUser); -> Assignee added to constructor
            taskId++;
            return a;   
        }

        public void LimitTasks(int columnId, int limitNum)
        {
            if (!is_UserLoggedin)
            {
                throw new Exception("User is not logged in");
            }
            if (columnId < 0 || columnId > columns.Count - 1)
            {
                throw new Exception("Invalid colomn Ordinal");
            }
            if(!loggedInUser.Equals(creatorEmail))
            {
                throw new Exception("only creator can do this");
            }
            columns[columnId].SetLimitNum(limitNum);
           
        }
        public void DeleteTask(int columnOrdinal, int taskId)
        {
            if (!loggedInUser.Equals(columns[columnOrdinal].getTasksDict()[taskId].getEmailAssignee()))
            {
                throw new Exception("only taskAsignee can do that");
            }
            if (columnId < 0 || columnId > columns.Count - 1)
            {
                throw new Exception("Invalid colomn Ordinal");
            }
            columns[columnOrdinal].DeleteTask(taskId);
        }

        public void AdvanceTask(int currentColId, int taskId)
        {
            if (!is_UserLoggedin)
            {
                throw new Exception("User is not logged in");
            }
            if (currentColId == columns.Count - 1) // if you're in the last column
            {
                throw new Exception("You can't advance tasks from the last column");
            }
            if(currentColId < 0 || currentColId > columns.Count-1)
            {
                throw new Exception("Invalid colomn Ordinal");
            }
            if (!loggedInUser.Equals(columns[currentColId].getTasksDict()[taskId].getEmailAssignee()))
            {
                throw new Exception("only taskAsignee can do that");
            }
            columns[currentColId + 1].AddTasksToDict(taskId, columns[currentColId].GetTaskById(taskId)); // add task to the next column
            columns[currentColId].DeleteTask(taskId); // delete task from current column
        }

        public void UpdateTaskDueDate(int colId, int taskId, DateTime dueDate)
        {
            if(colId == columns.Count)
            {
                throw new Exception("can't update tasks in done column");
            }
            if (colId < 0 || colId > columns.Count - 1)
            {
                throw new Exception("INVALID column id");
            }
            if (!loggedInUser.Equals(columns[colId].getTasksDict()[taskId].getEmailAssignee()))
            {
                throw new Exception("only taskAsignee can do that");
            }
            if (!is_UserLoggedin)
            {
                throw new Exception("User is not logged in");
            }
            columns[colId].GetTaskById(taskId).SetDueDate(dueDate);
        }
        public void UpdateTaskTitle(int colId, int taskId, string title)
        {
            if (colId == columns.Count)
            {
                throw new Exception("can't update tasks in done column");
            }
            if (colId < 0 || colId > columns.Count - 1)
            {
                throw new Exception("INVALID column id");
            }
            if (!loggedInUser.Equals(columns[colId].getTasksDict()[taskId].getEmailAssignee()))
            {
                throw new Exception("only taskAsignee can do that");
            }
            if (!is_UserLoggedin)
            {
                throw new Exception("User is not logged in");
            }
            columns[colId].GetTaskById(taskId).SetTitle(title);
        }

        public void UpdateTaskDescription(int colId, int taskId, string description)
        {
            if (colId == columns.Count)
            {
                throw new Exception("can't update tasks in done column");
            }
            if (colId < 0 || colId > columns.Count - 1)
            {
                throw new Exception("INVALID column id");
            }
            if (!loggedInUser.Equals(columns[colId].getTasksDict()[taskId].getEmailAssignee()))
            {
                throw new Exception("only taskAsignee can do that");
            }
            if (!is_UserLoggedin)
            {
                throw new Exception("User is not logged in");
            }
            columns[colId].GetTaskById(taskId).SetDescription(description);
        }

        public Column GetColumnById(int columnOrdinal)
        {
            if (columnOrdinal < 0)
            {
                throw new Exception("Invalid column ordinal");
            }
            if(columnOrdinal > columns.Count-1)
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
        
        public List<string> GetMyColumns()
        {
            List<string> columnsName = new List<string>();
            foreach(var c in columns)
            {
                columnsName.Add(c.Value.GetName());
            }
            return columnsName;
        }
        public Column AddColumn(int columnOrdinal, string Name)
        {
            if (columnOrdinal < 0|| columnOrdinal > columns.Count)
            {
                throw new Exception("Invalid column ordinal");
            }
            foreach(var col in columns)
            {
                if (col.Value.GetName().Equals(Name))
                {
                    throw new Exception("This name is allready taken");
                }
            }
            if (!loggedInUser.Equals(creatorEmail))
            {
                throw new Exception("only creator can do this");
            }
            for (int i = columns.Count-1; i >= columnOrdinal; i--) // moving all necessery columns right
            {

                columns[i].setColumnId(i + 1);
                
                Column col = columns[i];
                // ----!! We need to update each task in the TASK table in SQL in the column we are moving with the new column ID somehow
                
                columns.Remove(i);
                columns.Add(i+1, col);
                 
            }
            Column c = new Column(Name, columnOrdinal);
            columns.Add(columnOrdinal, c);
            columnId++;
            return c;
        }
        public void RemoveColumn(int columnOrdinal)
        {
            if (columnOrdinal < 0 || columnOrdinal > columns.Count - 1)
            {
                throw new Exception("Invalid column ordinal");
            }
            if (columns.Count == minColumns)
            {
                throw new Exception("You must have at least 2 column");
            }
            if (!loggedInUser.Equals(creatorEmail))
            {
                throw new Exception("only creator can do this");
            }
            Dictionary<int, Task> tasks = columns[columnOrdinal].getTasksDict(); // all tasks from the column to be removed

            if (columnOrdinal == 0) // move tasks to right column
            {
                if (columns[columnOrdinal + 1].GetNumOfTasks() + columns[columnOrdinal].GetNumOfTasks() > columns[columnOrdinal + 1].GetLimitNum()
                    && columns[columnOrdinal + 1].GetLimitNum() != -1) // if tasks num in right side column+task num in column to be removed are bigger than the limit of the right
                                                                       //side column OR there's no limit on the right side column
                {
                    throw new Exception("You cant delete this tasks because the number of tasks is too high");
                }
                foreach (var t in tasks)
                {
                    columns[columnOrdinal + 1].AddTasksToDict(t.Key, t.Value); // add tasks from the column 0 to the one on its right side

                }
            }
            else // move tasks to left column
            {
                if (columns[columnOrdinal - 1].GetNumOfTasks() + columns[columnOrdinal].GetNumOfTasks() > columns[columnOrdinal - 1].GetLimitNum()
                    && columns[columnOrdinal - 1].GetLimitNum() != -1)
                {
                    throw new Exception("You cant delete this tasks because the number of tasks is too high");
                }
                foreach (var t in tasks)
                {
                    columns[columnOrdinal - 1].AddTasksToDict(t.Key, t.Value); // add tasks from the column to be removed to the one on its left
                    //columns[columnOrdinal].myTaskDC.Update(t.Value.GetTaskId(), creatorEmail, DataAccessLayer.DTOs.TaskDTO.IDColumnName, columnOrdinal - 1);
                }

                //columns[columnOrdinal].removeMyTasks();
                columns.Remove(columnOrdinal);

                for (int i = columnOrdinal + 1; i <= columns.Count; i++) // moving all necessery columns left
                {
                    columns[i].setColumnId(i - 1);
                    Column col = columns[i];
                    columns.Remove(i);
                    columns.Add(i - 1, col);
                }
            }
        }
        public Column MoveColumnRight(int columnOrdinal) // we must update the SQL TASK table with new columnID for each task
        {
            if (columnOrdinal < 0 || columnOrdinal > columns.Count-1)
            {
                throw new Exception("Invalid column ordinal");
            }
            if (columnOrdinal == columns.Count-1)
            {
                throw new Exception("You cant move the most right column");
            }
            if (!loggedInUser.Equals(creatorEmail))
            {
                throw new Exception("only creator can do this");
            }

            columns[columnOrdinal].setColumnId(columnOrdinal + 1);
            columns[columnOrdinal + 1].setColumnId(columnOrdinal);
            Column col1 = columns[columnOrdinal]; // the one being moved right
            Column col2 = columns[columnOrdinal + 1];

            columns.Remove(columnOrdinal+1);
            columns.Add(columnOrdinal + 1, col1);
            columns.Remove(columnOrdinal);
            columns.Add(columnOrdinal, col2);
            return col1;
        }
        public Column MoveColumnLeft(int columnOrdinal)// we must update the SQL TASK table with new columnID for each task
        {
            if (columnOrdinal < 0 || columnOrdinal > columns.Count-1)
            {
                throw new Exception("Invalid column ordinal");
            }
            if (columnOrdinal == 0)
            {
                throw new Exception("You cant move the most left column");
            }
            if (!loggedInUser.Equals(creatorEmail))
            {
                throw new Exception("only creator can do this");
            }

            columns[columnOrdinal].setColumnId(columnOrdinal - 1);
            columns[columnOrdinal - 1].setColumnId(columnOrdinal);

            Column col1 = columns[columnOrdinal]; // the one being moved left
            Column col2 = columns[columnOrdinal - 1];

            columns.Remove(columnOrdinal-1);
            columns.Add(columnOrdinal - 1, col1);
            columns.Remove(columnOrdinal);
            columns.Add(columnOrdinal, col2);
            return col1;
        }
        public Task GetTaskById(int colID,int taskId)
        {
            return columns[colID].GetTaskById(taskId);
        }
        public void addColumnToDict(Column c)
        {
            columns.Add(c.GetColumnId(), c);
        }

        public void AssignTask(int columnOrdinal, int taskId, string emailAssignee)
        {
            if (columnOrdinal < 0 || columnOrdinal > columns.Count)
            {
                throw new Exception("Invalid column ordinal");
            }
            columns[columnOrdinal].AssignTask(taskId, emailAssignee);
        }

        public void ChangeColumnName(int columnOrdinal, string newName)
        {
            if (columnOrdinal < 0 || columnOrdinal > columns.Count-1)
            {
                throw new Exception("Invalid column ordinal");
            }
            if (!loggedInUser.Equals(creatorEmail))
            {
                throw new Exception("only creator can do this");
            }
            columns[columnOrdinal].setName(newName);
        }
        public void setTaskId(int id)
        {
            this.taskId = id;
        }

        public int getColumnId()
        {
            return this.columnId;
        }
        public void setColumnId(int id)
        {
            this.columnId = id;
        }
        public int getTaskId()
        {
            return this.taskId;
        }
        public Dictionary<int,Column> getMyColumns()
        {
            return this.columns;
        }
        public void setLoggedInUser(string email)
        {
            this.loggedInUser = email;
        }
        public void setCreator(string email)
        {
            this.creatorEmail = email;
        }

    }
}
