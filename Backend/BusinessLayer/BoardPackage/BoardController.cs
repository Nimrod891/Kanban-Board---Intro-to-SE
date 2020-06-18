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

        public Task AddNewTask(string userEmail ,string title, string description, DateTime dueDate)
        {

            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board does not exist");
            }

            Task a = boards[userEmail].AddNewTask(title, description, dueDate);
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
        }

        public void LimitTasks(string userEmail, int columnId, int limitNum)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board does not exist");
            }
            boards[userEmail].LimitTasks(columnId, limitNum);
            //boards[creatorEmail].ToDalObject().Save();
        }
        public void AdvanceTask(string userEmail, int currentColId, int taskId)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board does not exist");
            }
            boards[userEmail].AdvanceTask(currentColId, taskId);
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
            //boards[creatorEmail].ToDalObject().Save();
        }

        public void UpdateTaskTitle(string userEmail, int colId, int taskId, string title)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board not exist");
            }
            boards[userEmail].UpdateTaskTitle(colId, taskId, title);
            //boards[creatorEmail].ToDalObject().Save();
        }

        public void UpdateTaskDescription(string userEmail, int colId, int taskId, string description)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board not exist");
            }
            boards[userEmail].UpdateTaskDescription(colId, taskId, description);
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
            return boards[email].AddColumn(columnOrdinal, Name);
        }
        public void RemoveColumn(string email, int columnOrdinal)
        {
            if (!boards.ContainsKey(email))
            {
                throw new Exception("Board not exist");
            }
            boards[email].RemoveColumn(columnOrdinal);
        }
        public Column MoveColumnRight(string email, int columnOrdinal)
        {
            if (!boards.ContainsKey(email))
            {
                throw new Exception("Board not exist");
            }
            return boards[email].MoveColumnRight(columnOrdinal);
        }
        public Column MoveColumnLeft(string email, int columnOrdinal)
        {
            if (!boards.ContainsKey(email))
            {
                throw new Exception("Board not exist");
            }
            return boards[email].MoveColumnLeft(columnOrdinal);
        }
        public Task GetTaskById(string email,int colID, int taskId)
        {
            return boards[email].GetTaskById(colID, taskId);
        }
    }
}
