using System;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    /// <summary>
    /// The service for using the Kanban board.
    /// It allows executing all of the required behaviors by the Kanban board.
    /// You are not allowed (and can't due to the interfance) to change the signatures
    /// Do not add public methods\members! Your client expects to use specifically these functions.
    /// You may add private, non static fields (if needed).
    /// You are expected to implement all of the methods.
    /// Good luck.
    /// </summary>
    public class Service : IService
    {
        boardService myBoardService;
        userService myUserService;
        /// <summary>
        /// Simple public constructor.
        /// </summary>
        
        public Service()
        {
            myBoardService = new boardService();
            myUserService = new userService();



        }
        /// <summary>        
        /// Loads the data. Intended be invoked only when the program starts
        /// </summary>
        /// <returns>A response object. The response should contain a error message in case of an error.</returns>

        public Response LoadData()
        {
            myUserService.LoadData();
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="email">The email address of the user to register</param>
        /// <param name="password">The password of the user to register</param>
        /// <param name="nickname">The nickname of the user to register</param>
        /// <returns>A response object. The response should contain a error message in case of an error<returns>
        public Response Register(string email, string password, string nickname)
        {

            return myUserService.Register(email, password, nickname);
        }

        /// <summary>
        /// Log in an existing user
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response object with a value set to the user, instead the response should 
        /// contain a error message in case of an error</returns>
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
            return myBoardService.GetColumn(email, columnOrdinal);
        }
    }
}
