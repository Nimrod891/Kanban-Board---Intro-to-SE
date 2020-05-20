using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class UserDalController: DalController
    {
        private const string MessageTableName = "USER";
        public UserDalController() : base(MessageTableName)
        {

        }
        protected List<DTOs.UserDTO> Select(int id, string email)
        {
            List<DTOs.UserDTO> results = new List<DTOs.UserDTO>();
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
        public List<DTOs.UserDTO> SelectAllusers()
        {
            //string t_name = "boards";
            List<DTOs.UserDTO> results = new List<DTOs.UserDTO>();
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

        public bool Insert(DTOs.UserDTO User)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {MessageTableName} ({DTOs.DTO.IDColumnName} ,{DTOs.UserDTO.MessageEmailColumnName},{DTOs.UserDTO.MessageNickNameColumnName},{DTOs.UserDTO.MessagePassword}) " +
                        $"VALUES (@idVal,@emailval,@nicknameVal,@passwordval);";

                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", User.Id);
                    SQLiteParameter nameParam = new SQLiteParameter(@"passwordVal", User.NickName);

                    SQLiteParameter passwordParam = new SQLiteParameter(@"nicknameval", User.NickName);
                    SQLiteParameter emailparam = new SQLiteParameter(@"emailval", User.email);

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

        protected DTOs.UserDTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            DTOs.UserDTO result = new DTOs.UserDTO((long)reader.GetValue(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
            return result;

        }


    }
}
