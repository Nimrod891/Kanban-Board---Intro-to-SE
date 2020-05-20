using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class ColumnDalController : DalController
    {
        private const string MessageTableName = "Column";
        public ColumnDalController() : base(MessageTableName)
        {

        }


        public List<DTOs.BoardDTO> SelectAllColumns(int id, string email)
        {
            List<DTOs.BoardDTO> result = Select(id, email).Cast<DTOs.BoardDTO>().ToList();

            return result;
        }

        public List<DTOs.ColumnDTO> Select(int id, string email)
        {
            //string t_name = "columns";
            List<DTOs.ColumnDTO> results = new List<DTOs.ColumnDTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {MessageTableName} WHERE email = {email} AND id= {id}";
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

        public bool Insert(DTOs.ColumnDTO column)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {MessageTableName} ({DTOs.DTO.IDColumnName} ,{DTOs.ColumnDTO.MessageLimitNumColumnName},{DTOs.ColumnDTO.MessageBoardIDColumnName},{DTOs.ColumnDTO.MessageNameColumnName},{DTOs.ColumnDTO.MessageNumTasksColumnName}) " +
    $"VALUES (@idVal,@LimitNumVal,@BoardIDVal,@ColumnNameVal,@NumTaskval);";
                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", column.Id);
                    SQLiteParameter limitParam = new SQLiteParameter(@"LimitNumVal", column.LimitNum);
                    SQLiteParameter boardParam = new SQLiteParameter(@"BoardIDVal", column.BoardID);
                    SQLiteParameter nameParam = new SQLiteParameter(@"ColumnNameVal", column.Name);
                    SQLiteParameter numParam = new SQLiteParameter(@"NumTaskval", column.NumTasks);




                    command.Parameters.Add(idParam);
                    command.Parameters.Add(nameParam);
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

        protected DTOs.ColumnDTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            DTOs.ColumnDTO result = new DTOs.ColumnDTO((long)reader.GetValue(0), (long)reader.GetValue(1), (long)reader.GetValue(2), reader.GetString(3), (long)reader.GetValue(4), reader.GetString(5));

            return result;

        }
    }
}
