using System;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class Service : IService
    {
        boardService myBoardService;
        userService myUserService;
        public Service()
        {
        }

        public Response LoadData()
        {
            try
            {
                myBoardService = new boardService();
                myUserService = new userService();
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
            
        }
        public Response Register(string email, string password, string nickname)
        {

            return myUserService.Register(email, password, nickname);
        }

        public Response<User> Login(string email, string password)
        {
            return myUserService.Login(email, password);
        }

        public Response Logout(string email)
        {
            return myUserService.Logout(email);
        }
        public Response<Board> GetBoard(string email)
        {
            return myBoardService.GetBoard(email);
        }
        public Response LimitColumnTasks(string email, int columnOrdinal, int limit)
        {
            return myBoardService.LimitColumnTasks(email, columnOrdinal, limit);
        }
        public Response<Task> AddTask(string email, string title, string description, DateTime dueDate)
        {

            return myBoardService.AddTask(email, title, description, dueDate);
        }
        public Response UpdateTaskDueDate(string email, int columnOrdinal, int taskId, DateTime dueDate)
        {
            return myBoardService.UpdateTaskDueDate(email, columnOrdinal, taskId, dueDate);
        }
        public Response UpdateTaskTitle(string email, int columnOrdinal, int taskId, string title)
        {
            return myBoardService.UpdateTaskTitle(email, columnOrdinal, taskId, title);
        }
        public Response UpdateTaskDescription(string email, int columnOrdinal, int taskId, string description)
        {
            return myBoardService.UpdateTaskDescription(email, columnOrdinal, taskId, description);
        }
        public Response AdvanceTask(string email, int columnOrdinal, int taskId)
        {
            return myBoardService.AdvanceTask(email, columnOrdinal, taskId);
        }
        public Response<Column> GetColumn(string email, string columnName)
        {
            return myBoardService.GetColumn(email, columnName);
        }

        public Response<Column> GetColumn(string email, int columnOrdinal)
        {
            return myBoardService.GetColumn(email,columnOrdinal);
        }
    }
}
