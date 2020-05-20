using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    class Board
    {
       
        private static int num_Boards;
        private int Id_board;
        private string userEmail;
        private Dictionary<int, Column> columns;
        private int taskId;
        private int columnId;
        private int minColumns = 2;
        private bool is_UserLoggedin;
        DataAccessLayer.ColumnDalController myColumnDC;

        public Board(string userEmail)
        {
            this.userEmail = userEmail;
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
       
        public void initBoard()
        {
            List<DataAccessLayer.DTOs.ColumnDTO> myColumns = myColumnDC.Select(Id_board, userEmail);
            foreach (DataAccessLayer.DTOs.ColumnDTO c in myColumns)
            {
                Column newCol = new Column(c.Name, c.Id);
                columns.Add(c.Id, newCol);
                newCol.initColumn(userEmail);
            }
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
            Task a = columns[0].AddTask(taskId, title, description, dueDate);
            taskId++;
            return a;   
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
            if (currentColId == columns.Count - 1) // if you're in the last column
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
            if(colId == columns.Count)
            {
                throw new Exception("can't update tasks in done column");
            }
            if (is_UserLoggedin)
                columns[colId].GetTaskById(taskId).SetDueDate(dueDate);
            else
                throw new Exception("User is not logged in");
        }
        public void UpdateTaskTitle(int colId, int taskId, string title)
        {
            if (colId == columns.Count)
            {
                throw new Exception("can't update tasks in done column");
            }
            if (is_UserLoggedin)
                columns[colId].GetTaskById(taskId).SetTitle(title);
            else
                throw new Exception("User is not logged in");
        }

        public void UpdateTaskDescription(int colId, int taskId, string description)
        {
            if (colId == columns.Count)
            {
                throw new Exception("can't update tasks in done column");
            }
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
            if (columnOrdinal < 0 || columnOrdinal > columns.Count)
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
            for(int i = columnOrdinal; i <= columns.Count; i++) // moving all necessery columns right
            {
                columns[i].setColumnId(i + 1);
                Column col = columns[i];
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
            if(columnOrdinal<0 || columnOrdinal > columns.Count)
            {
                throw new Exception("Invalid column ordinal");
            }
            if (columns.Count == minColumns)
            {
                throw new Exception("You must have at least 2 column");
            }
            Dictionary<int, Task> tasks = columns[columnOrdinal].getMyTasks();
            
            if (columnOrdinal == 0) // move tasks to right column
            {
                if(columns[columnOrdinal+1].GetNumOfTasks()+columns[columnOrdinal].GetNumOfTasks() > columns[columnOrdinal + 1].GetLimitNum())
                {
                    throw new Exception("You cant delete this tasks because the number of tasks is too high");
                }
                foreach (var t in tasks)
                {
                    columns[columnOrdinal + 1].AddTasksToDict(t.Key, t.Value);
                }
            }
            else
            {
                if (columns[columnOrdinal - 1].GetNumOfTasks() + columns[columnOrdinal].GetNumOfTasks() > columns[columnOrdinal - 1].GetLimitNum())
                {
                    throw new Exception("You cant delete this tasks because the number of tasks is too high");
                }
                foreach (var t in tasks)
                {
                    columns[columnOrdinal - 1].AddTasksToDict(t.Key, t.Value);
                }
            }
            columns[columnOrdinal].removeMyTasks();
            columns.Remove(columnOrdinal);
            for (int i = columnOrdinal+1; i <= columns.Count; i++) // moving all necessery columns left
            {
                columns[i].setColumnId(i - 1);
                Column col = columns[i];
                columns.Remove(i);
                columns.Add(i - 1, col);
            }
        }
        public Column MoveColumnRight(int columnOrdinal)
        {
            if (columnOrdinal < 0 || columnOrdinal > columns.Count)
            {
                throw new Exception("Invalid column ordinal");
            }
            if (columnOrdinal == columns.Count)
            {
                throw new Exception("You cant move the most right column");
            }
            columns[columnOrdinal].setColumnId(columnOrdinal + 1);
            columns[columnOrdinal + 1].setColumnId(columnOrdinal);
            Column col1 = columns[columnOrdinal];
            Column col2 = columns[columnOrdinal + 1];
            columns.Remove(columnOrdinal);
            columns.Add(columnOrdinal + 1, col1);
            columns.Remove(columnOrdinal + 1);
            columns.Add(columnOrdinal, col2);
            return col1;
        }
        public Column MoveColumnLeft(int columnOrdinal)
        {
            if (columnOrdinal < 0 || columnOrdinal > columns.Count)
            {
                throw new Exception("Invalid column ordinal");
            }
            if (columnOrdinal == 0)
            {
                throw new Exception("You cant move the most left column");
            }
            columns[columnOrdinal].setColumnId(columnOrdinal - 1);
            columns[columnOrdinal - 1].setColumnId(columnOrdinal);
            Column col1 = columns[columnOrdinal];
            Column col2 = columns[columnOrdinal - 1];
            columns.Remove(columnOrdinal);
            columns.Add(columnOrdinal - 1, col1);
            columns.Remove(columnOrdinal - 1);
            columns.Add(columnOrdinal, col2);
            return col1;
        }
    }
}
