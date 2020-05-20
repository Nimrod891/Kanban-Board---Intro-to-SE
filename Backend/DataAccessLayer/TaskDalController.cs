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

        public List<DTOs.TaskDTO> SelectAllTasks(int id, string email)
        {
            List<DTOs.TaskDTO> result = Select(id, email).Cast<DTOs.TaskDTO>().ToList();

            return result;
        }

        public List<DTOs.TaskDTO> Select(int id, string email)
        {
            //string t_name = "columns";
            List<DTOs.TaskDTO> results = new List<DTOs.TaskDTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {MessageTableName} WHERE email = {email} AND id={id} ";
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


        public bool Insert(DTOs.TaskDTO TASK)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {MessageTableName} ({DTOs.DTO.IDColumnName} ,{DTOs.TaskDTO.MessageTitleColumnName},{DTOs.TaskDTO.MessagedescriptionColumnName},{DTOs.TaskDTO.MessageDueDateColumnName},{DTOs.TaskDTO.MessageDueDateColumnName}{DTOs.TaskDTO.MessageCreationTimeColumnName}{DTOs.TaskDTO.MessagecolumnColumnName}) " +
    $"VALUES (@idVal,@Titelval,@descriptionVal,@DueDateVal,@CreationVal,@ColumnIdVal);";
                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", TASK.Id);
                    SQLiteParameter titleParam = new SQLiteParameter(@"Titelval", TASK.Description);
                    SQLiteParameter descriptionParam = new SQLiteParameter(@"descriptionVal", TASK.DueDate);
                    SQLiteParameter duedateParam = new SQLiteParameter(@"DueDateVal", TASK.DueDate);
                    SQLiteParameter creationtimeParam = new SQLiteParameter(@"CreationVal", TASK.CreationTime);
                    SQLiteParameter columnideParam = new SQLiteParameter(@"ColumnIdVal", TASK.ColumnId);





                    command.Parameters.Add(idParam);
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
            DTOs.TaskDTO result = new DTOs.TaskDTO((long)reader.GetValue(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetDateTime(4), (long)reader.GetValue(5), reader.GetString(6));
            return result;

        }

    }
}
