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
        private Dictionary<string, Board> boards;
        //private Board loggedInBoard;

        public BoardController()
        {
            boards = new Dictionary<string, Board>();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Kanban JSON Files", "Boards");
            Directory.CreateDirectory(path);
            foreach (string file in Directory.EnumerateFiles(path, "*.json"))
            {
                Board boardToAdd = new Board(DataAccessLayer.Objects.Board.FromJson(file));
                //boardToAdd = Board.FromJson(Read(file));
                boards.Add(boardToAdd.GetUserEmail(), boardToAdd);

                /// if boards exist in the folder /Kanban JSON Files/Boards 
                /// this will create a dictionary of {email, board} as a field in BoardController

            }
        }

        public Board GetBoard(string userEmail)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board not exist");
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
            boards[userEmail].ToDalObject().Save();

            return a;
            
        }

        public void LimitTasks(string userEmail, int columnId, int limitNum)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board not exist");
            }
            boards[userEmail].LimitTasks(columnId, limitNum);
        }
        public void AdvanceTask(string userEmail, int currentColId, int taskId)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board not exist");
            }
            boards[userEmail].AdvanceTask(currentColId, taskId);
            boards[userEmail].ToDalObject().Save();

        }

        

        public void UpdateTaskDueDate(string userEmail, int colId, int taskId, DateTime dueDate)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board not exist");
            }
            boards[userEmail].UpdateTaskDueDate(colId, taskId, dueDate);
        }

        public void UpdateTaskTitle(string userEmail, int colId, int taskId, string title)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board not exist");
            }
            boards[userEmail].UpdateTaskTitle(colId, taskId, title);
        }

        public void UpdateTaskDescription(string userEmail, int colId, int taskId, string description)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board not exist");
            }
            boards[userEmail].UpdateTaskDescription(colId, taskId, description);
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
        
        /*public IReadOnlyCollection<Task> GetColumns(string userEmail, string columnName)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board not exist");
            }
            int colId = boards[userEmail].GetColumnByName(columnName);
            return boards[userEmail].GetColumnById(colId).GetMyTasks();
        }
        */
    }
}
