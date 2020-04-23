using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    class BoardController
    {
        private Dictionary<string, Board> boards;

        public BoardController()
        {
            boards = new Dictionary<string, Board>();
        }

        public IReadOnlyCollection<string> GetBoard(string userEmail)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board not exist");
            }
            ReadOnlyCollection<string> readOnlyCollection = new );
        }

        public void AddNewTask(string userEmail ,string title, string description, DateTime dueDate)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board not exist");
            }
            boards[userEmail].AddNewTask(title, description, dueDate);
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

        public void GetColumnById(string userEmail, int columnOrdinal)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board not exist");
            }
            boards[userEmail].GetColumnById(columnOrdinal);
        }

        public void GetColumnByName(string userEmail, string colName)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("Board not exist");
            }
            boards[userEmail].GetColumnByName(colName);
        }
    }
}
