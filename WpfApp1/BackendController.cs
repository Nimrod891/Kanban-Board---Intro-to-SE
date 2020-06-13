using System;
using System.Windows;
using WpfApp1.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace WpfApp1
{
    class BackendController
    {
        public IService Service { get; private set; }
        // לבדוק לגבי Dalcontroler כאן
        public BackendController(IService service)
        {
            this.Service = service;
        }

        public BackendController()
        {
            this.Service = new Service();
            Service.LoadData();
        }

        public void Login(string username, string password)
        {
            Response<User> user = Service.Login(username, password);
            if (user.ErrorOccured)
            {
                throw new Exception(user.ErrorMessage);
            }

        }

        internal void Logout(string email)
        {
           Response res= Service.Logout(email);
            if(res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }
        internal BoardModel GetBoard(string email)
        {
            Response<Board> res = Service.GetBoard(email);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return new BoardModel(this,email);

        }

        internal void Register(string username, string password,string nickname)
        {
            Response res = Service.Register(username, password,nickname);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }
        internal void LimitColumnTasks(string email,int columnOrdianl,int limit)
        {
            Response res = Service.LimitColumnTasks(email,columnOrdianl,limit);

            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }
        internal TaskModel AddTask(string email,string titile,string description,DateTime dueDate,int col)
        {
            var res = Service.AddTask(email,titile,description,dueDate);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return new TaskModel(this, res.Value.Id, res.Value.Title, res.Value.DueDate, res.Value.CreationTime, res.Value.Description, email,col);
        }
        /*
        internal void DelteTask(string email, int cloid,int taskid)
        {
            var res = Service.DeleteTask(email, cloid,taskid);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            MessageBox.Show($"Task {taskid} in columns {cloid} was changed successfully");

        }
        */


        internal void UpdateTaskDueDate(string email, int columnOrdinal, int taskId, DateTime dueDate)
        {
            Response res = Service.UpdateTaskDueDate(email,columnOrdinal,taskId,dueDate);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }
        internal void UpdateTaskTitle(string email, int columnOrdinal, int taskId, string title)
        {
            Response res = Service.UpdateTaskTitle(email, columnOrdinal, taskId, title);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }
        internal void UpdateTaskDescription(string email, int columnOrdinal, int taskId,string description)
        {
            Response res = Service.UpdateTaskDescription(email, columnOrdinal, taskId, description);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            MessageBox.Show("Title was changed successfully");

        }
        internal void AdvanceTask(string email, int columnOrdinal, int taskId)
        {
            Response res = Service.AdvanceTask(email, columnOrdinal, taskId);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            MessageBox.Show("Task was advanced successfully");

        }
        internal Response<IntroSE.Kanban.Backend.ServiceLayer.Task> GetTask(string email, int column_ordinal , int task_id)
        {
            Response<IntroSE.Kanban.Backend.ServiceLayer.Task> res = Service.GetTask(email, column_ordinal, task_id);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res;
           // return new TaskModel(this, res.Value, email);

        }
        internal void GetColumn(string email, string columnName)
        {
            Response<Column> res = Service.GetColumn(email,columnName);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }

        }

        internal Response<Column> GetColumn(string email, int columnName)
        {
            Response<Column> res = Service.GetColumn(email, columnName);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res;
            //return new ColumnModel(this, email, res.Value.id_column, res.Value.Name, res.Value.Limit, res.Value.Tasks);
        }
        internal void RemoveColumn(string email, int columnOrdinal)
        {
            Response res = Service.RemoveColumn(email, columnOrdinal);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            MessageBox.Show("Column was removed successfully");

        }
        internal void MoveColumnRight(string email, int columnOrdinal)
        {
            Response<Column> res = Service.MoveColumnRight(email, columnOrdinal);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            MessageBox.Show("Column was moved right successfully");

        }
        internal void AddColumn(string email, int columnOrdinal,string Name)
        {
            Response<Column> res = Service.AddColumn(email, columnOrdinal,Name);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            MessageBox.Show("Column was Added successfully");

        }
        internal void MoveColumnLeft(string email, int columnOrdinal)
        {
            Response<Column> res = Service.MoveColumnLeft(email, columnOrdinal);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            MessageBox.Show("Column was moved Left successfully");

        }











/*
        internal void RemoveMessage(string email, int id)
        {
            Response res = Service.RemoveMessage(email, id);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            MessageBox.Show("Message was removed successfully");
        }
        */
    }
}
