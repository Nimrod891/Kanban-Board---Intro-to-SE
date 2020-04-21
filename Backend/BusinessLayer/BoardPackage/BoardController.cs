using System;
using System.Collections.Generic;
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

        public Board GetBoard(string userEmail)
        {
            return boards[userEmail];
        }

        public void AddNewTask(string userEmail ,string title, string description, DateTime dueDate)
        {
            boards[userEmail].AddNewTask(title, description, dueDate);
        }

        public void LimitTasks(string userEmail, int columnId, int limitNum)
        {
            boards[userEmail].LimitTasks(columnId, limitNum);
        }
        public void AdvanceTask(string userEmail, int currentColId, int taskId)
        {
            boards[userEmail].AdvanceTask(currentColId, taskId);
        }

        public void UpdateTaskDueDate(string userEmail, int colId, int taskId, DateTime dueDate)
        {
            boards[userEmail].UpdateTaskDueDate(colId, taskId, dueDate);
        }

        public void UpdateTaskTitle(string userEmail, int colId, int taskId, string title)
        {
            boards[userEmail].UpdateTaskTitle(colId, taskId, title);
        }

        public void UpdateTaskDescription(string userEmail, int colId, int taskId, string description)
        {
            boards[userEmail].UpdateTaskDescription(colId, taskId, description);
        }

        public void GetColumnById(string userEmail, int columnOrdinal)
        {
            boards[userEmail].GetColumnById(columnOrdinal);
        }

        public void GetColumnByName(string userEmail, string colName)
        {
            boards[userEmail].GetColumnByName(colName);
        }

    }
}
