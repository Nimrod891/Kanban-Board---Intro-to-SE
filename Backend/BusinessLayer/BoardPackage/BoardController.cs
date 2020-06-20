using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    class BoardController
    {
        private DataAccessLayer.BoardDalController myBoardDC;
        private DataAccessLayer.ColumnDalController myColumnDC;
        private DataAccessLayer.TaskDalController myTaskDC;
        private Dictionary<string, Board> boards;
        private Board loggedInBoard;

        public BoardController()
        {
            boards = new Dictionary<string, Board>();
        }

        public void LoadData()
        {
            myBoardDC = new DataAccessLayer.BoardDalController();
            myColumnDC = new DataAccessLayer.ColumnDalController();
            myTaskDC = new DataAccessLayer.TaskDalController();
            List<DataAccessLayer.DTOs.BoardDTO> myBoards = myBoardDC.SelectAllboards();
            foreach (DataAccessLayer.DTOs.BoardDTO b in myBoards)
            {
                Board newBoard = new Board(0, b.email);
                boards.Add(newBoard.GetUserEmail(), newBoard);
                List<DataAccessLayer.DTOs.ColumnDTO> myColumns = myColumnDC.SelectAllColumns(newBoard.GetUserEmail());
                foreach (DataAccessLayer.DTOs.ColumnDTO c in myColumns)
                {
                    int newId = Convert.ToInt32(c.Id);
                    int newLimit = Convert.ToInt32(c.LimitNum);
                    int newNumTasks = Convert.ToInt32(c.NumTasks);
                    Column newCol = new Column(c.email, c.Name, newId, newLimit, newNumTasks);
                    newBoard.addColumnToDict(newCol);
                    List<DataAccessLayer.DTOs.TaskDTO> myTasks = myTaskDC.SelectAllTasks(newBoard.GetUserEmail(), newCol.GetColumnId());
                    foreach (DataAccessLayer.DTOs.TaskDTO t in myTasks)
                    {
                        int tId = Convert.ToInt32(t.Id);
                        Task newTask = new Task(tId, t.Title, t.Description, t.DueDate, t.CreationTime);
                        newCol.AddTasksToDict(newTask.GetTaskId(), newTask);
                        newBoard.setTaskId(newCol.GetMyTasks().Count + newBoard.getTaskId());
                    }
                }
            }
        }
        public void AssignTask(string email, int columnOrdinal, int taskId, string emailAssignee)
        {
            if (!boards.ContainsKey(email))
            {
                throw new Exception("Board does not exist");
            }
            boards[email].AssignTask(columnOrdinal, taskId, emailAssignee);
        }

        public void DeleteTask(string email, int columnOrdinal, int taskId)
        {
            if (!boards.ContainsKey(email))
            {
                throw new Exception("Board does not exist");
            }
            boards[email].DeleteTask(columnOrdinal, taskId);
            myTaskDC.Delete(taskId, email);
        }
        public Board GetBoard(string userEmail)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board does not exist");
            }
            return boards[userEmail];
        }

        public Task AddNewTask(string userEmail, string title, string description, DateTime dueDate)
        {

            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board does not exist");
            }

            Task a = boards[userEmail].AddNewTask(title, description, dueDate);
            DataAccessLayer.DTOs.TaskDTO dataTask = new DataAccessLayer.DTOs.TaskDTO(a.GetTaskId(), 0, userEmail, a.GetTitle(), a.GetDescription(), a.GetDueDate(), a.GetCreationDate());
            myTaskDC.Insert(dataTask);
            //boards[creatorEmail].ToDalObject().Save();

            return a;

        }
        public void ChangeColumnName(string email, int columnOrdinal, string newName)
        {
            if (!boards.ContainsKey(email))
            {
                throw new Exception("Board does not exist");
            }
            boards[email].ChangeColumnName(columnOrdinal, newName);
            myColumnDC.Update(columnOrdinal, email, DataAccessLayer.DTOs.ColumnDTO.MessageNameColumnName, newName);
        }

        public void LimitTasks(string userEmail, int columnId, int limitNum)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board does not exist");
            }
            boards[userEmail].LimitTasks(columnId, limitNum);
            myColumnDC.Update(columnId, userEmail, DataAccessLayer.DTOs.ColumnDTO.MessageLimitNumColumnName, limitNum);
            //boards[creatorEmail].ToDalObject().Save();
        }
        public void AdvanceTask(string userEmail, int currentColId, int taskId)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board does not exist");
            }
            boards[userEmail].AdvanceTask(currentColId, taskId);
            myTaskDC.Update(taskId, userEmail, DataAccessLayer.DTOs.TaskDTO.MessagecolumnColumnName, currentColId + 1);
            //boards[creatorEmail].ToDalObject().Save();

        }

        public void SetLoggedInBoard(string email)
        {
            this.loggedInBoard = boards[email];
            boards[email].SetIsULoggedIn(true);
        }



        public void UpdateTaskDueDate(string userEmail, int colId, int taskId, DateTime dueDate)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board not exist");
            }
            boards[userEmail].UpdateTaskDueDate(colId, taskId, dueDate);
            myTaskDC.Update(taskId, userEmail, DataAccessLayer.DTOs.TaskDTO.MessageDueDateColumnName, dueDate);
            //boards[creatorEmail].ToDalObject().Save();
        }

        public void UpdateTaskTitle(string userEmail, int colId, int taskId, string title)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board not exist");
            }
            boards[userEmail].UpdateTaskTitle(colId, taskId, title);
            myTaskDC.Update(taskId, userEmail, DataAccessLayer.DTOs.TaskDTO.MessageTitleColumnName, title);
            //boards[creatorEmail].ToDalObject().Save();
        }

        public void UpdateTaskDescription(string userEmail, int colId, int taskId, string description)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board not exist");
            }
            boards[userEmail].UpdateTaskDescription(colId, taskId, description);
            myTaskDC.Update(taskId, userEmail, DataAccessLayer.DTOs.TaskDTO.MessagedescriptionColumnName, description);
            //boards[creatorEmail].ToDalObject().Save();
        }

        public Column GetColumnById(string userEmail, int columnOrdinal)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board not exist");
            }
            return boards[userEmail].GetColumnById(columnOrdinal);
        }

        public Column GetColumnByName(string userEmail, string colName)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board not exist");
            }
            return boards[userEmail].GetColumnByName(colName);
        }
        public Column AddColumn(string email, int columnOrdinal, string Name)
        {
            if (!boards.ContainsKey(email))
            {
                throw new Exception("Board not exist");
            }
            Column c = boards[email].AddColumn(columnOrdinal, Name);
            for (int i = boards[email].getMyColumns().Count - 1; i > columnOrdinal; i--)// update tasks columnid,columnid,instert new column.
            {
                foreach (var t in boards[email].getMyColumns()[i].getMyTasks())
                {
                    myTaskDC.Update(t.Value.GetTaskId(), email, DataAccessLayer.DTOs.TaskDTO.MessagecolumnColumnName, i);
                }
                myColumnDC.Update(i - 1, email, DataAccessLayer.DTOs.ColumnDTO.IDColumnName, i);
            }
            DataAccessLayer.DTOs.ColumnDTO dataColumn = new DataAccessLayer.DTOs.ColumnDTO(columnOrdinal, email, c.GetLimitNum(), c.GetName(), c.GetNumOfTasks());
            myColumnDC.Insert(dataColumn);
            return c;
        }
        public void RemoveColumn(string email, int columnOrdinal)
        {
            if (!boards.ContainsKey(email))
            {
                throw new Exception("Board not exist");
            }
            boards[email].RemoveColumn(columnOrdinal);
            if (columnOrdinal == 0)
            {
                foreach (var t in boards[email].getMyColumns()[columnOrdinal + 1].getMyTasks())
                {
                    myTaskDC.Update(t.Value.GetTaskId(), email, DataAccessLayer.DTOs.TaskDTO.MessagecolumnColumnName, columnOrdinal + 1);
                }
            }
            else
            {
                foreach (var t in boards[email].getMyColumns()[columnOrdinal - 1].getMyTasks())
                    myTaskDC.Update(t.Value.GetTaskId(), email, DataAccessLayer.DTOs.TaskDTO.MessagecolumnColumnName, columnOrdinal - 1);
            }

            myColumnDC.Delete(columnOrdinal, email);
            for (int i = columnOrdinal + 1; i <= boards[email].getMyColumns().Count; i++)
            {
                foreach (var t in boards[email].getMyColumns()[i - 1].getMyTasks())
                {
                    myTaskDC.Update(t.Value.GetTaskId(), email, DataAccessLayer.DTOs.TaskDTO.MessagecolumnColumnName, i - 1);
                }
                myColumnDC.Update(i, email, DataAccessLayer.DTOs.ColumnDTO.IDColumnName, i - 1);
            }
        }
        public Column MoveColumnRight(string email, int columnOrdinal)
        {
            if (!boards.ContainsKey(email))
            {
                throw new Exception("Board not exist");
            }
            Dictionary<int, Task> taskscol1 = boards[email].getMyColumns()[columnOrdinal].getMyTasks();
            Dictionary<int, Task> taskscol2 = boards[email].getMyColumns()[columnOrdinal + 1].getMyTasks();
            Column c = boards[email].MoveColumnRight(columnOrdinal);
            foreach (var t in taskscol1)
            {
                myTaskDC.Update(t.Value.GetTaskId(), email, DataAccessLayer.DTOs.TaskDTO.MessagecolumnColumnName, columnOrdinal + 1);
            }
            foreach (var t in taskscol2)
            {
                myTaskDC.Update(t.Value.GetTaskId(), email, DataAccessLayer.DTOs.TaskDTO.MessagecolumnColumnName, columnOrdinal);
            }
            myColumnDC.Update(columnOrdinal + 1, email, DataAccessLayer.DTOs.ColumnDTO.IDColumnName, -1);
            myColumnDC.Update(columnOrdinal, email, DataAccessLayer.DTOs.ColumnDTO.IDColumnName, columnOrdinal + 1);
            myColumnDC.Update(-1, email, DataAccessLayer.DTOs.ColumnDTO.IDColumnName, columnOrdinal);
            return c;
        }
        public Column MoveColumnLeft(string email, int columnOrdinal)
        {
            if (!boards.ContainsKey(email))
            {
                throw new Exception("Board not exist");
            }
            Dictionary<int, Task> taskscol1 = boards[email].getMyColumns()[columnOrdinal].getMyTasks();
            Dictionary<int, Task> taskscol2 = boards[email].getMyColumns()[columnOrdinal - 1].getMyTasks();
            Column c = boards[email].MoveColumnLeft(columnOrdinal);
            foreach (var t in taskscol1)
            {
                myTaskDC.Update(t.Value.GetTaskId(), email, DataAccessLayer.DTOs.TaskDTO.MessagecolumnColumnName, columnOrdinal - 1);
            }
            foreach (var t in taskscol2)
            {
                myTaskDC.Update(t.Value.GetTaskId(), email, DataAccessLayer.DTOs.TaskDTO.MessagecolumnColumnName, columnOrdinal);
            }
            myColumnDC.Update(columnOrdinal - 1, email, DataAccessLayer.DTOs.ColumnDTO.IDColumnName, -1);
            myColumnDC.Update(columnOrdinal, email, DataAccessLayer.DTOs.ColumnDTO.IDColumnName, columnOrdinal - 1);
            myColumnDC.Update(-1, email, DataAccessLayer.DTOs.ColumnDTO.IDColumnName, columnOrdinal);
            return c;
        }
        public Task GetTaskById(string email, int colID, int taskId)
        {
            return boards[email].GetTaskById(colID, taskId);
        }
        public Dictionary<string,Board> getMyBoards()
        {
            return this.boards;
        }
        public void addBoardToDict(Board b)
        {
            this.boards.Add(b.GetUserEmail(), b);
        }
    }
}
