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


        public List<DTOs.UserDTO> SelectAllUsers()
        {
            List<DTOs.UserDTO> result = Select().Cast<DTOs.UserDTO>().ToList();

            return result;
        }

        public dictionary loadUsers

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

        protected override DTOs.DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            DTOs.UserDTO result = new DTOs.UserDTO((long)reader.GetValue(0), reader.GetString(1), reader.GetString(2), (long)reader.GetValue(3));
            return result;

        }
    }

}
}
