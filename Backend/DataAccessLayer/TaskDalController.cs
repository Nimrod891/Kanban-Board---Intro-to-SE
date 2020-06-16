using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class TaskDalController : DalController
    {

        private const string MessageTableName = "Task";
        public TaskDalController() : base(MessageTableName)
        {

        }

        public List<DTOs.TaskDTO> SelectAllTasks(string email, int columnid)
        {
            //string t_name = "columns";
            List<DTOs.TaskDTO> results = new List<DTOs.TaskDTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {MessageTableName} WHERE email = '{email}' AND Column={columnid}";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToObject(dataReader));

                    }
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                }

            }
            return results;
        }

        public DTOs.TaskDTO Select(int id, string email, int columnid)
        {
            //string t_name = "columns";
            DTOs.TaskDTO t;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {MessageTableName} WHERE email = '{email}' AND Column = {columnid} AND id={id} ";
                SQLiteDataReader dataReader = null;
                
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                   
                       t= ConvertReaderToObject(dataReader);

                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                }

            }
            return t;
        }


        public bool Insert(DTOs.TaskDTO TASK)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {MessageTableName} ({DTOs.DTO.IDColumnName} ,{DTOs.TaskDTO.MessagecolumnColumnName},{DTOs.TaskDTO.EmailColumnName},{DTOs.TaskDTO.MessageTitleColumnName},{DTOs.TaskDTO.MessagedescriptionColumnName},{DTOs.TaskDTO.MessageDueDateColumnName},{DTOs.TaskDTO.MessageCreationTimeColumnName}) " +
    $"VALUES (@idVal,@Columnval,@emailval,@Titleval,@Descriptionval,@DueDateVal,@CreationTimeVal);";
                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", TASK.Id);
                    SQLiteParameter emailParam = new SQLiteParameter(@"emailval", TASK.email);
                    SQLiteParameter titleParam = new SQLiteParameter(@"Titleval", TASK.Title);
                    SQLiteParameter descriptionParam = new SQLiteParameter(@"Descriptionval", TASK.Description);
                    SQLiteParameter duedateParam = new SQLiteParameter(@"DueDateVal", TASK.DueDate);
                    SQLiteParameter creationtimeParam = new SQLiteParameter(@"CreationTimeVal", TASK.CreationTime);
                    SQLiteParameter columnideParam = new SQLiteParameter(@"ColumnVal", TASK.ColumnId);



                    command.Parameters.Add(idParam);
                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(titleParam);
                    command.Parameters.Add(descriptionParam);
                    command.Parameters.Add(duedateParam);
                    command.Parameters.Add(creationtimeParam);
                    command.Parameters.Add(columnideParam);
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                }
                catch
                {
                    //log error
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return res > 0;
            }
        }

        protected DTOs.TaskDTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            DTOs.TaskDTO result;
            if (reader.IsDBNull(4))
            {
                result = new DTOs.TaskDTO((long)reader.GetValue(0), (long)reader.GetValue(1), reader.GetString(2),
                reader.GetString(3), null, reader.GetDateTime(5), reader.GetDateTime(6));
            }
            else
            {
                result = new DTOs.TaskDTO((long)reader.GetValue(0), (long)reader.GetValue(1), reader.GetString(2),
                reader.GetString(3), reader.GetString(4), reader.GetDateTime(5), reader.GetDateTime(6));
            }
            return result;
        }
    }
}
