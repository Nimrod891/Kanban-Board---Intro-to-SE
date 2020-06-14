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
        private string emailHost;
        private Dictionary<int, Column> columns;
        private int taskId;
        private int columnId;
        private int minColumns = 2;
        private bool is_UserLoggedin;
        private DataAccessLayer.BoardDalController myBoardDC;
        private DataAccessLayer.ColumnDalController myColumnDC;
        //private DataAccessLayer.TaskDalController myTaskDC;

        public Board(string userEmail)//create new board
        {
            myBoardDC = new DataAccessLayer.BoardDalController();
            myColumnDC = new DataAccessLayer.ColumnDalController();
            columns = new Dictionary<int, Column>();
            this.userEmail = userEmail;
            this.emailHost = userEmail;
            DataAccessLayer.DTOs.BoardDTO dataBoard = new DataAccessLayer.DTOs.BoardDTO(this.Id_board, this.userEmail);
            myBoardDC.Insert(dataBoard);
            
            columnId = 0;
            Column backlog = new Column("backlog", columnId);
            DataAccessLayer.DTOs.ColumnDTO dataColumn = new DataAccessLayer.DTOs.ColumnDTO(backlog.GetColumnId(), userEmail, backlog.GetLimitNum(), backlog.GetName(), backlog.GetNumOfTasks());
            myColumnDC.Insert(dataColumn);
            columnId++;
            Column in_progress = new Column("in progress", columnId);
            DataAccessLayer.DTOs.ColumnDTO dataColumn1 = new DataAccessLayer.DTOs.ColumnDTO(in_progress.GetColumnId(), userEmail, in_progress.GetLimitNum(), in_progress.GetName(), in_progress.GetNumOfTasks());
            myColumnDC.Insert(dataColumn1);
            columnId++;
            Column done = new Column("done", columnId);
            DataAccessLayer.DTOs.ColumnDTO dataColumn2 = new DataAccessLayer.DTOs.ColumnDTO(done.GetColumnId(), userEmail, done.GetLimitNum(), done.GetName(), done.GetNumOfTasks());
            myColumnDC.Insert(dataColumn2);
            columns.Add(backlog.GetColumnId(), backlog);
            columns.Add(in_progress.GetColumnId(), in_progress);
            columns.Add(done.GetColumnId(), done);
            taskId = 0;
            is_UserLoggedin = false;
            

        }
       
        public Board(int id, string userEmail) // load existing board
        {
            columns = new Dictionary<int, Column>();
            this.userEmail = userEmail;

            //is_UserLoggedin = false;
            myColumnDC = new DataAccessLayer.ColumnDalController();
            List<DataAccessLayer.DTOs.ColumnDTO> myColumns = myColumnDC.SelectAllColumns( userEmail);
               // Select(Id_board, userEmail);
            foreach (DataAccessLayer.DTOs.ColumnDTO c in myColumns)
            {
                int newId = Convert.ToInt32(c.Id);
                int newLimit = Convert.ToInt32(c.LimitNum);
                int newNumTasks = Convert.ToInt32(c.NumTasks);
                Column newCol = new Column(c.email, c.Name, newId, newLimit, newNumTasks);
                columns.Add(newId, newCol);
                
            }
           
        }
        public void setEmailHost(string emailHost)
        {
            this.emailHost = emailHost;
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
            DataAccessLayer.DTOs.TaskDTO dataTask = new DataAccessLayer.DTOs.TaskDTO(taskId, 0, userEmail, title, description, dueDate, DateTime.Now);
            columns[0].myTaskDC.Insert(dataTask);
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
            myColumnDC.Update(columnId,userEmail, DataAccessLayer.DTOs.ColumnDTO.MessageLimitNumColumnName, limitNum);
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
            columns[currentColId].myTaskDC.Update(taskId, userEmail, DataAccessLayer.DTOs.TaskDTO.MessagecolumnColumnName, currentColId+1);
            columns[currentColId].DeleteTask(taskId); // delete task from current column
        }

        public void UpdateTaskDueDate(int colId, int taskId, DateTime dueDate)
        {
            if(colId == columns.Count)
            {
                throw new Exception("can't update tasks in done column");
            }
            if (is_UserLoggedin)
            {
                columns[colId].GetTaskById(taskId).SetDueDate(dueDate);
                columns[columnId].myTaskDC.Update(taskId, userEmail, DataAccessLayer.DTOs.TaskDTO.MessageDueDateColumnName, dueDate);
            }
              
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
            {
                columns[colId].GetTaskById(taskId).SetTitle(title);
                columns[colId].myTaskDC.Update(taskId, userEmail, DataAccessLayer.DTOs.TaskDTO.MessageTitleColumnName, title);
            }
                
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
            {
                columns[colId].GetTaskById(taskId).SetDescription(description);
                columns[colId].myTaskDC.Update(taskId, userEmail, DataAccessLayer.DTOs.TaskDTO.MessagedescriptionColumnName, description);
            }
                

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
            for(int i = columns.Count-1; i >= columnOrdinal; i--) // moving all necessery columns right
            {

                columns[i].setColumnId(i + 1);
                
                Column col = columns[i];
                // ----!! We need to update each task in the TASK table in SQL in the column we are moving with the new column ID somehow
                
                columns.Remove(i);
                columns.Add(i+1, col);
                //foreach(var t in )
                foreach (var TaskDictionaryPair in col.getMyTasks())
                {

                    col.myTaskDC.Update(TaskDictionaryPair.Value.GetTaskId(), userEmail, DataAccessLayer.DTOs.TaskDTO.MessagecolumnColumnName, 
                       col.GetColumnId()); // old columnid in SQL to updated one
                }
                myColumnDC.Update(i, userEmail, DataAccessLayer.DTOs.ColumnDTO.IDColumnName, i + 1);
                 
            }
            Column c = new Column(Name, columnOrdinal);
            columns.Add(columnOrdinal, c);
            DataAccessLayer.DTOs.ColumnDTO dataColumn = new DataAccessLayer.DTOs.ColumnDTO(columnOrdinal, userEmail, c.GetLimitNum(), c.GetName(), c.GetNumOfTasks());
            myColumnDC.Insert(dataColumn);
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
                if(columns[columnOrdinal+1].GetNumOfTasks()+columns[columnOrdinal].GetNumOfTasks() > columns[columnOrdinal + 1].GetLimitNum()
                    && columns[columnOrdinal + 1].GetLimitNum() != -1) // if tasks num in right side column+task num in column to be removed are bigger than the limit of the right
                    //side column OR there's no limit on the right side column
                {
                    throw new Exception("You cant delete this tasks because the number of tasks is too high");
                }
                foreach (var t in tasks)
                {
                    columns[columnOrdinal + 1].AddTasksToDict(t.Key, t.Value);
                    //columns[columnOrdinal].myTaskDC.Update(t.Value.GetTaskId(), userEmail, DataAccessLayer.DTOs.TaskDTO.IDColumnName, columnOrdinal + 1);
                    columns[columnOrdinal].myTaskDC.Update(t.Value.GetTaskId(), userEmail, DataAccessLayer.DTOs.TaskDTO.MessagecolumnColumnName,
                       columnOrdinal+1);
                }
            }
            else // move tasks to left column
            {
                if (columns[columnOrdinal - 1].GetNumOfTasks() + columns[columnOrdinal].GetNumOfTasks() > columns[columnOrdinal - 1].GetLimitNum() 
                    && columns[columnOrdinal - 1].GetLimitNum()!=-1)
                {
                    throw new Exception("You cant delete this tasks because the number of tasks is too high");
                }
                foreach (var t in tasks)
                {
                    columns[columnOrdinal - 1].AddTasksToDict(t.Key, t.Value);
                    //columns[columnOrdinal].myTaskDC.Update(t.Value.GetTaskId(), userEmail, DataAccessLayer.DTOs.TaskDTO.IDColumnName, columnOrdinal - 1);
                    columns[columnOrdinal].myTaskDC.Update(t.Value.GetTaskId(), userEmail, DataAccessLayer.DTOs.TaskDTO.MessagecolumnColumnName,
                       columnOrdinal - 1);
                }
            }

            columns[columnOrdinal].removeMyTasks();
            DataAccessLayer.DTOs.ColumnDTO dataColumn = new DataAccessLayer.DTOs.ColumnDTO(columnOrdinal, userEmail, columns[columnOrdinal].GetLimitNum(),
                columns[columnOrdinal].GetName(), columns[columnOrdinal].GetNumOfTasks());

            myColumnDC.Delete(dataColumn, userEmail);
            columns.Remove(columnOrdinal);

            for (int i = columnOrdinal+1; i <= columns.Count; i++) // moving all necessery columns left
            {
                columns[i].setColumnId(i - 1);
                Column col = columns[i];
                foreach (var TaskDictionaryPair in col.getMyTasks())
                {
                    col.myTaskDC.Update(TaskDictionaryPair.Value.GetTaskId(), userEmail, DataAccessLayer.DTOs.TaskDTO.MessagecolumnColumnName,
                       col.GetColumnId()); // old columnid in SQL to updated one
                }
                columns.Remove(i);
                columns.Add(i - 1, col);
                myColumnDC.Update(i, userEmail, DataAccessLayer.DTOs.ColumnDTO.IDColumnName, i - 1);
            }
        }
        public Column MoveColumnRight(int columnOrdinal) // we must update the SQL TASK table with new columnID for each task
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
            Column col1 = columns[columnOrdinal]; // the one being moved right
            foreach (var TaskDictionaryPair in col1.getMyTasks())
            {

                col1.myTaskDC.Update(TaskDictionaryPair.Value.GetTaskId(), userEmail, DataAccessLayer.DTOs.TaskDTO.MessagecolumnColumnName,
                   col1.GetColumnId()); // old columnid in SQL to updated one
            }
            Column col2 = columns[columnOrdinal + 1];
            foreach (var TaskDictionaryPair in col2.getMyTasks())
            {

                col2.myTaskDC.Update(TaskDictionaryPair.Value.GetTaskId(), userEmail, DataAccessLayer.DTOs.TaskDTO.MessagecolumnColumnName,
                   col2.GetColumnId()); // old columnid in SQL to updated one
            }

            myColumnDC.Update(columnOrdinal+1, userEmail, DataAccessLayer.DTOs.ColumnDTO.IDColumnName, -1);
            myColumnDC.Update(columnOrdinal, userEmail, DataAccessLayer.DTOs.ColumnDTO.IDColumnName, columnOrdinal+1);
            myColumnDC.Update(-1, userEmail, DataAccessLayer.DTOs.ColumnDTO.IDColumnName, columnOrdinal);

            columns.Remove(columnOrdinal+1);
            columns.Add(columnOrdinal + 1, col1);
            columns.Remove(columnOrdinal);
            columns.Add(columnOrdinal, col2);
            return col1;
        }
        public Column MoveColumnLeft(int columnOrdinal)// we must update the SQL TASK table with new columnID for each task
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

            Column col1 = columns[columnOrdinal]; // the one being moved left
            foreach (var TaskDictionaryPair in col1.getMyTasks())
            {

                col1.myTaskDC.Update(TaskDictionaryPair.Value.GetTaskId(), userEmail, DataAccessLayer.DTOs.TaskDTO.MessagecolumnColumnName,
                   col1.GetColumnId()); // old columnid in SQL to updated one
            }
            Column col2 = columns[columnOrdinal - 1];
            foreach (var TaskDictionaryPair in col2.getMyTasks())
            {

                col2.myTaskDC.Update(TaskDictionaryPair.Value.GetTaskId(), userEmail, DataAccessLayer.DTOs.TaskDTO.MessagecolumnColumnName,
                   col2.GetColumnId()); // old columnid in SQL to updated one
            }

            myColumnDC.Update(col2.GetColumnId(), userEmail, DataAccessLayer.DTOs.ColumnDTO.IDColumnName, -1);
            myColumnDC.Update(col1.GetColumnId(), userEmail, DataAccessLayer.DTOs.ColumnDTO.IDColumnName, col1.GetColumnId() + 1);
            myColumnDC.Update(-1, userEmail, DataAccessLayer.DTOs.ColumnDTO.IDColumnName, col2.GetColumnId() - 1);
            columns.Remove(columnOrdinal-1);
            columns.Add(columnOrdinal - 1, col1);
            columns.Remove(columnOrdinal);
            columns.Add(columnOrdinal, col2);
            return col1;
        }
    }
}
