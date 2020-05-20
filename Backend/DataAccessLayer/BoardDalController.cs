using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class BoardDalController : DalController
    {
        private const string MessageTableName = "Board";
        public BoardDalController() : base(MessageTableName)
        {
        }


        public List<DTOs.BoardDTO> SelectAllboards()
        {
            //string t_name = "boards";
            List<DTOs.BoardDTO> results = new List<DTOs.BoardDTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {MessageTableName}";
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

        public List<DTOs.BoardDTO> Select(int id, string email)
        {
            //string t_name = "boards";
            List<DTOs.BoardDTO> results = new List<DTOs.BoardDTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                if (email.Equals("***"))
                {
                    command.CommandText = $"select * from {MessageTableName};";
                }
                else
                {
                    command.CommandText = $"select * from {MessageTableName} WHERE email = {email};";
                }

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


        public bool Insert(DTOs.BoardDTO BOARD)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {MessageTableName} ({DTOs.DTO.IDColumnName} ,{DTOs.BoardDTO.EmailColumnName}) " +
                        $"VALUES (@idVal,@emailVal);";

                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", BOARD.Id);
                    //SQLiteParameter nameParam = new SQLiteParameter(@"nameVal", BOARD.Name); ASK NIV

                    command.Parameters.Add(idParam);
                    //command.Parameters.Add(nameParam); ASK NIV
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

        protected DTOs.BoardDTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            DTOs.BoardDTO result = new DTOs.BoardDTO((long)reader.GetValue(0), reader.GetString(1));
            return result;

        }

    }
}
