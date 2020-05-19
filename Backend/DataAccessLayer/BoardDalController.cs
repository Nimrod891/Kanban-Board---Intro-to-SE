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


        public List<DTOs.BoardDTO> SelectAllForums()
        {
            List<DTOs.BoardDTO> result = Select().Cast<DTOs.BoardDTO>().ToList();

            return result;
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
                    command.CommandText = $"INSERT INTO {MessageTableName} ({DTOs.DTO.IDColumnName} ,{DTOs.BoardDTO.BoardNameColumnName}) " +
                        $"VALUES (@idVal,@nameVal);";

                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", BOARD.Id);
                    SQLiteParameter nameParam = new SQLiteParameter(@"nameVal", BOARD.Name);

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

        protected override DTOs.BoardDTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            DTOs.BoardDTO result = new DTOs.BoardDTO((long)reader.GetValue(0), reader.GetString(1));
            return result;

        }
    }
}
