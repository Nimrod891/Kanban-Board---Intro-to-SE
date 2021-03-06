using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    class boardService
    {
        BusinessLayer.BoardPackage.BoardController MyBoardContorller;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public boardService()
        {
            this.MyBoardContorller = new BusinessLayer.BoardPackage.BoardController();
        }
        public void SetLoggedInBoard(string email)
        {
            MyBoardContorller.SetLoggedInBoard(email);
        }
        public Response<Board> GetBoard(string email)
        {
            try
            {
                List<string> columnNames = new List<string>();
                columnNames = MyBoardContorller.GetBoard(email).GetMyColumns();
                ReadOnlyCollection<string> colNames = new ReadOnlyCollection<string>(columnNames);
                Board boardService = new Board(colNames);
                return new Response<Board>(boardService);
            }
            catch (Exception e)
            {

                return new Response<Board>(e.Message);
            }
        }
        public Response LoadData()
        {
            try
            {
                MyBoardContorller.LoadData();
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        public Response AssignTask(string email, int columnOrdinal, int taskId, string emailAssignee)
        {
            try
            {
                MyBoardContorller.AssignTask(email, columnOrdinal, taskId, emailAssignee);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        public Response DeleteTask(string email, int columnOrdinal, int taskId)
        {
            try
            {
                MyBoardContorller.DeleteTask(email, columnOrdinal, taskId);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
            public Response LimitColumnTasks(string email, int columnOrdinal, int limit)
        {
            try
            {
                MyBoardContorller.LimitTasks(email,columnOrdinal,limit);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response ChangeColumnName(string email, int columnOrdinal, string newName)
        {
            try
            {
                MyBoardContorller.ChangeColumnName(email, columnOrdinal, newName);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        public Response<Task> AddTask(string email, string title, string description, DateTime dueDate)
        {
            try
            {
                BusinessLayer.BoardPackage.Task t = MyBoardContorller.AddNewTask(email, title, description, dueDate);
                Task servicTask = new Task(t.GetTaskId(),t.GetCreationDate(),t.GetDueDate(),t.GetTitle(),t.GetDescription(), t.getEmailAssignee());
                log.Info($"User {email} has added a new task: \n{title}\nDue Date:{dueDate}");
                return new Response<Task>(servicTask);
            }
            catch (Exception e)
            {
                log.Error("User " + email + " tried to add a task with a due date in the past");
                return new Response<Task>(e.Message);
            }
        }
        public Response UpdateTaskDueDate(string email, int columnOrdinal, int taskId, DateTime dueDate)
        {
            try
            {
                MyBoardContorller.UpdateTaskDueDate(email, columnOrdinal, taskId, dueDate);
                log.Info($"User {email} has updated: COLUMN {columnOrdinal}, TASK {taskId} due date");
                return new Response();
            }
            catch (Exception e)
            {
                log.Error($"User {email} has failed to update COLUMN {columnOrdinal}, TASK {taskId} " +
                    $"\nDue date: {dueDate}");
                return new Response(e.Message);
            }
        }
        public Response UpdateTaskTitle(string email, int columnOrdinal, int taskId, string title)
        {
            try
            {
                MyBoardContorller.UpdateTaskTitle(email, columnOrdinal, taskId, title);
                log.Info($"User {email} has updated: COLUMN {columnOrdinal}, TASK {taskId} title");
                return new Response();
            }
            catch (Exception e)
            {
                log.Error($"User {email} has failed to update COLUMN {columnOrdinal}, TASK {taskId} " +
                    $"\nTitle: {title}");
                return new Response(e.Message);
            }
        }
        public Response UpdateTaskDescription(string email, int columnOrdinal, int taskId, string description)
        {
            try
            {
                MyBoardContorller.UpdateTaskDescription(email, columnOrdinal, taskId, description);
                log.Info($"User {email} has updated: COLUMN: {columnOrdinal}, TASK {taskId}: description");
                return new Response();
            }
            catch (Exception e)
            {
                log.Error($"User {email} has failed to update task description: COLUMN: {columnOrdinal}, TASK {taskId}:" +
                    $" \nDescription: {description}");
                return new Response(e.Message);
            }
        }

        public Response AdvanceTask(string email, int columnOrdinal, int taskId)
        {
            try
            {
                MyBoardContorller.AdvanceTask(email,columnOrdinal,taskId);
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
            return new Response();
        }
        public Response<Column> GetColumn(string email, string columnName)
        {
            try
            {
                Column columnService = new Column(MyBoardContorller.GetColumnByName(email, columnName).GetMyTasks(), columnName, MyBoardContorller.GetColumnByName(email, columnName).GetLimitNum());
                return new Response<Column>(columnService);
            }
            catch (Exception e)
            {
                return new Response<Column>(e.Message);
            }
        }
        public Response<Column> GetColumn(string email, int columnOrdinal)
        {
            try
            {
                Column columnService = new Column(MyBoardContorller.GetColumnById(email, columnOrdinal).GetMyTasks(), MyBoardContorller.GetColumnById(email,columnOrdinal).GetName(), MyBoardContorller.GetColumnById(email, columnOrdinal).GetLimitNum());
                return new Response<Column>(columnService);
            }
            catch (Exception e)
            {
                return new Response<Column>(e.Message);
            }
        }
        public Response RemoveColumn(string email, int columnOrdinal)
        {
            try
            {
                MyBoardContorller.RemoveColumn(email, columnOrdinal);
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
            return new Response();
        }
        public Response<Column> AddColumn(string email, int columnOrdinal, string Name)
        {
            try
            {
                BusinessLayer.BoardPackage.Column c = MyBoardContorller.AddColumn(email, columnOrdinal, Name);
                Column columnService = new Column(MyBoardContorller.GetColumnById(email, columnOrdinal).GetMyTasks(), c.GetName(), c.GetLimitNum());
                return new Response<Column>(columnService);
            }
            catch (Exception e)
            {
                return new Response<Column>(e.Message);
            }
        }
        public Response<Column> MoveColumnRight(string email, int columnOrdinal)
        {
            try
            {
                BusinessLayer.BoardPackage.Column c = MyBoardContorller.MoveColumnRight(email, columnOrdinal);
                Column columnService = new Column(MyBoardContorller.GetColumnById(email, columnOrdinal).GetMyTasks(), c.GetName(), c.GetLimitNum());
                return new Response<Column>(columnService);
            }
            catch (Exception e)
            {
                return new Response<Column>(e.Message);
            }
        }
        public Response<Column> MoveColumnLeft(string email, int columnOrdinal)
        {
            try
            {
                BusinessLayer.BoardPackage.Column c = MyBoardContorller.MoveColumnLeft(email, columnOrdinal);
                Column columnService = new Column(MyBoardContorller.GetColumnById(email, columnOrdinal).GetMyTasks(), c.GetName(), c.GetLimitNum());
                return new Response<Column>(columnService);
            }
            catch (Exception e)
            {
                return new Response<Column>(e.Message);
            }
        }
        public Response<Task> GetTaskById(string email, int colID, int taskId)
        {
            try
            {
                BusinessLayer.BoardPackage.Task t = MyBoardContorller.GetTaskById(email, colID, taskId);
                Task taskService = new Task(t.GetTaskId(), t.GetCreationDate(), t.GetDueDate(), t.GetTitle(), t.GetDescription(), t.getEmailAssignee());
                return new Response<Task>(taskService);
            }
            catch (Exception e)
            {
                return new Response<Task>(e.Message);
            }
        }
        public BusinessLayer.BoardPackage.BoardController getMyBoardController()
        {
            return this.MyBoardContorller;
        }
    }
}
