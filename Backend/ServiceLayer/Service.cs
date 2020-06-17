using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
           (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        boardService myBoardService;
        userService myUserService;
        

        /// <summary>
        /// Simple public constructor.
        /// </summary>
        public Service()
        {
            this.myUserService = new userService();
            this.myBoardService = new boardService();
            
        }
               
        /// <summary>        
        /// Loads the data. Intended be invoked only when the program starts
        /// </summary>
        /// <returns>A response object. The response should contain a error message in case of an error.</returns>
        public Response LoadData()
        {
            if(!File.Exists(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "M3.db"))))
                {
                string sql1 = @"CREATE TABLE Board(id INTEGER NOT NULL,email TEXT NOT NULL PRIMARY KEY )";
                string sql2 = "CREATE TABLE USER(id INTEGER NOT NULL,email TEXT NOT NULL PRIMARY KEY,NickName TEXT NOT NULL, password TEXT NOT NULL)";
                string sql3 = "CREATE TABLE Column(id INTEGER NOT NULL,email text NOT NULL ,LimitNum INTEGER NOT NULL, Name TEXT NOT NULL, NumTask INTEGER NOT NULL,PRIMARY KEY ('id','email'))";
                string sql4 = "CREATE TABLE Task(id INTEGER NOT NULL,Column INTEGER NOT NULL,email TEXT NOT NULL ,Title TEXT NOT NULL , Description TEXT,DueDate DATETIME NOT NULL,CreationTime DATETIME NOT NULL,PRIMARY KEY('id','Column','email' ))";
                System.Data.SQLite.SQLiteConnection.CreateFile("M3.db");


                //using (System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("data source=M3.db"))
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "M3.db"));
                string connectionString = $"Data Source={path}; Version=3;";
                using (System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection(connectionString))
                {
                    using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                    {
                        try
                        {
                            con.Open();

                            com.CommandText = sql1;
                            com.ExecuteNonQuery();
                            com.CommandText = sql2;
                            com.ExecuteNonQuery();
                            com.CommandText = sql3;
                            com.ExecuteNonQuery();
                            com.CommandText = sql4;
                            com.ExecuteNonQuery();
                        }
                        finally
                        {
                            com.Dispose();
                            con.Close();
                        }
                        
                    }
                }

            }
            try
            {
                log.Info("Loading data");
                myUserService.LoadData();
                myBoardService.LoadData();

                return new Response();
            }
            catch (Exception e)
            {
                log.Fatal("Couldn't load data");
                return new Response(e.Message);
            }



        }


        ///<summary>Remove all persistent data.</summary>
        public Response DeleteData()
        {
            Response r = myUserService.DeleteData();
            return r;
        }



    public Response Register(string email, string password, string nickname)
        {

            Response r = myUserService.Register(email, password, nickname);
            if (r != null)
            {
                this.myBoardService.LoadData();
            }            
            log.Info("New User Registered: [" + email + "]");
            return r;
        }

        /// <summary>
        /// Log in an existing user
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response object with a value set to the user, instead the response should contain a error message in case of an error</returns>

        public Response Register(string email, string password, string nickname, string emailHost)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Assigns a task to a user
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>        
        /// <param name="emailAssignee">Email of the user to assign to task to</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AssignTask(string email, int columnOrdinal, int taskId, string emailAssignee)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete a task
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>        		
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response DeleteTask(string email, int columnOrdinal, int taskId)
        {
            throw new NotImplementedException();
        }


        public Response<User> Login(string email, string password)
        {
            Response<User> r = myUserService.Login(email, password);
            if (r.ErrorMessage == null)
                myBoardService.SetLoggedInBoard(email);

            return r;
        }

        /// <summary>        
        /// Log out an logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response Logout(string email)
        {
            return myUserService.Logout(email);
        }

        /// <summary>
        /// Returns the board of a user. The user must be logged in
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <returns>A response object with a value set to the board, instead the response should contain a error message in case of an error</returns>
        public Response<Board> GetBoard(string email)
        {
            return myBoardService.GetBoard(email);
           
        }

        /// <summary>
        /// Limit the number of tasks in a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response LimitColumnTasks(string email, int columnOrdinal, int limit)
        {
            return myBoardService.LimitColumnTasks(email, columnOrdinal, limit);
        }

        /// <summary>
        /// Add a new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>A response object with a value set to the Task, instead the response should contain a error message in case of an error</returns>
        public Response<Task> AddTask(string email, string title, string description, DateTime dueDate)
        {
            return myBoardService.AddTask(email, title, description, dueDate);
        }

        /// <summary>
        /// Update the due date of a task
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDueDate(string email, int columnOrdinal, int taskId, DateTime dueDate)
        {
            return myBoardService.UpdateTaskDueDate(email, columnOrdinal, taskId, dueDate);
        }

        /// <summary>
        /// Update task title
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskTitle(string email, int columnOrdinal, int taskId, string title)
        {
            return myBoardService.UpdateTaskTitle(email, columnOrdinal, taskId, title);
        }

        /// <summary>
        /// Update the description of a task
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDescription(string email, int columnOrdinal, int taskId, string description)
        {
            return myBoardService.UpdateTaskDescription(email, columnOrdinal, taskId, description);
        }

        /// <summary>
        /// Advance a task to the next column
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AdvanceTask(string email, int columnOrdinal, int taskId)
        {
            return myBoardService.AdvanceTask(email, columnOrdinal, taskId);
        }


        /// <summary>
        /// Returns a column given it's name
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnName">Column name</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>
        public Response<Column> GetColumn(string email, string columnName)
        {
            return myBoardService.GetColumn(email, columnName);
        }

        /// <summary>
        /// Returns a column given it's identifier.
        /// The first column is identified by 0, the ID increases by 1 for each column
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">Column ID</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>

        public Response<Column> GetColumn(string email, int columnOrdinal)
        {
            return myBoardService.GetColumn(email, columnOrdinal);
        }

        /// <summary>
        /// Removes a column given it's identifier.
        /// The first column is identified by 0, the ID increases by 1 for each column
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">Column ID</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response RemoveColumn(string email, int columnOrdinal)
        {
            return myBoardService.RemoveColumn(email, columnOrdinal);
        }

        /// <summary>
        /// Adds a new column, given it's name and a location to place it.
        /// The first column is identified by 0, the ID increases by 1 for each column        
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">Location to place to column</param>
        /// <param name="Name">new Column name</param>
        /// <returns>A response object with a value set to the new Column, the response should contain a error message in case of an error</returns>
        public Response<Column> AddColumn(string email, int columnOrdinal, string Name)
        {
            return myBoardService.AddColumn(email, columnOrdinal, Name);
        }

        /// <summary>
        /// Moves a column to the right, swapping it with the column wich is currently located there.
        /// The first column is identified by 0, the ID increases by 1 for each column        
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">Current location of the column</param>
        /// <returns>A response object with a value set to the moved Column, the response should contain a error message in case of an error</returns>
        public Response<Column> MoveColumnRight(string email, int columnOrdinal)
        {
            return myBoardService.MoveColumnRight(email, columnOrdinal);
        }

        /// <summary>
        /// Moves a column to the left, swapping it with the column wich is currently located there.
        /// The first column is identified by 0, the ID increases by 1 for each column.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">Current location of the column</param>
        /// <returns>A response object with a value set to the moved Column, the response should contain a error message in case of an error</returns>
        public Response<Column> MoveColumnLeft(string email, int columnOrdinal)
        {
            return myBoardService.MoveColumnLeft(email, columnOrdinal);
        }

        Response IService.ChangeColumnName(string email, int columnOrdinal, string newName)
        {
            throw new NotImplementedException();
        }
        public Response<Task> GetTaskById(string email, int colID, int taskId)
        {
            return myBoardService.GetTaskById(email, colID, taskId);
        }
    }
}
