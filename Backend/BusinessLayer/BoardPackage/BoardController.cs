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
        private Dictionary<string, Board> boards;
        private Board loggedInBoard;

        public BoardController()
        {
            myBoardDC = new DataAccessLayer.BoardDalController();
            boards = new Dictionary<string, Board>();
            List<DataAccessLayer.DTOs.BoardDTO> myBoards = myBoardDC.SelectAllboards();
            foreach (DataAccessLayer.DTOs.BoardDTO b in myBoards)
            {
                Board newBoard = new Board(0, b.email);
               
                boards.Add(b.email, newBoard);

            }
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
            //boards[userEmail].ToDalObject().Save();

            return a;
            
        }

        public void LimitTasks(string userEmail, int columnId, int limitNum)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board does not exist");
            }
            boards[userEmail].LimitTasks(columnId, limitNum);
            //boards[userEmail].ToDalObject().Save();
        }
        public void AdvanceTask(string userEmail, int currentColId, int taskId)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board does not exist");
            }
            boards[userEmail].AdvanceTask(currentColId, taskId);
            //boards[userEmail].ToDalObject().Save();

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
            //boards[userEmail].ToDalObject().Save();
        }

        public void UpdateTaskTitle(string userEmail, int colId, int taskId, string title)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board not exist");
            }
            boards[userEmail].UpdateTaskTitle(colId, taskId, title);
            //boards[userEmail].ToDalObject().Save();
        }

        public void UpdateTaskDescription(string userEmail, int colId, int taskId, string description)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board not exist");
            }
            boards[userEmail].UpdateTaskDescription(colId, taskId, description);
            //boards[userEmail].ToDalObject().Save();
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
    }
}
