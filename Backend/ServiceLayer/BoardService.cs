using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    class boardService
    {
        BusinessLayer.BoardPackage.BoardController MyBoardContorller;

        public boardService()
        {
            this.MyBoardContorller = new BusinessLayer.BoardPackage.BoardController();
        }
        public Response<Board> GetBoard(string email)
        {
            Response<Board> toReturn;
            try
            {
                Board boardService = new Board(MyBoardContorller.GetBoard(email));
                toReturn = new Response<Board>(boardService); ;
            }
            catch (Exception e)
            {

                toReturn = new Response<Board>(e.Message);
            }
            return toReturn;

        }
        public Response LimitColumnTasks(string email, int columnOrdinal, int limit)
        {
            try
            {
                MyBoardContorller.GetBoard(email).GetColumnById(columnOrdinal).SetLimitNum(limit);

            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
            return new Response();

        }
        public Response<Task> AddTask(string email, string title, string description, DateTime dueDate)
        {
            Task t = new Task();//לשנות
            try
            {
                MyBoardContorller.AddNewTask(email, title, description, dueDate);

            }
            catch (Exception e)
            {
                return new Response<Task>(t, e.Message);
            }
            return new Response<Task>(t);
        }
        public Response UpdateTaskDueDate(string email, int columnOrdinal, int taskId, DateTime dueDate)
        {
            try
            {
                MyBoardContorller.UpdateTaskDueDate(email, columnOrdinal, taskId, dueDate);

            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
            return new Response();
        }
        public Response UpdateTaskTitle(string email, int columnOrdinal, int taskId, string title)
        {
            try
            {
                MyBoardContorller.UpdateTaskTitle(email, columnOrdinal, taskId, title);

            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
            return new Response();
        }
        public Response UpdateTaskDescription(string email, int columnOrdinal, int taskId, string description)
        {
            try
            {
                MyBoardContorller.UpdateTaskTitle(email, columnOrdinal, taskId, description);

            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
            return new Response();
        }

        public Response AdvanceTask(string email, int columnOrdinal, int taskId)
        {
            try
            {
                MyBoardContorller.AdvanceTask(email, columnOrdinal, taskId);

            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
            return new Response();
        }
        public Response<Column> GetColumn(string email, string columnName)
        {
            Column t = new Column();
            try
            {
                MyBoardContorller.GetColumnByName(email, columnName);

            }
            catch (Exception e)
            {
                return new Response<Column>(t, e.Message);
            }
            return new Response<Column>(t);
        }

        public Response<Column> GetColumn(string email, int columnOrdinal)
        {
            Column t = new Column();
            try
            {
                MyBoardContorller.GetColumnById(email, columnOrdinal);

            }
            catch (Exception e)
            {
                return new Response<Column>(t, e.Message);
            }
            return new Response<Column>(t);
        }
    }
}
