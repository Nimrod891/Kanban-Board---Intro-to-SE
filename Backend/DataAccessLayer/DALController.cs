using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer.Objects;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal abstract class DalController
    {
        protected readonly string _connectionString;
        private readonly string _tableName;
        public DalController(string tableName)
        {
            try
            {
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "M3.db"));
                this._connectionString = $"Data Source={path}; Version=3;";
            }
            catch (Exception e)
            {
                if (e.Equals(null))
                {
                    string sql1 = @"CREATE TABLE Board(id INTEGER NOT NULL,email TEXT PRIMARY KEY NOT NULL)";
                    string sql2 = "CREATE TABLE USER(id INTEGER NOT NULL,email TEXT NOT NULL PRIMARY KEY,NickName TEXT NOT NULL ,password  TEXT NOT NULL)";
                    string sql3 = "CREATE TABLE Column(id INTEGER NOT NULL PRIMARY KEY,email TEXT NOT NULL PRIMARY KEY,limitNum INTEGER NOT NULL , BoardID INTEGER NOT NULL, Name TEXT NOT NULL, NumTasks INTEGER NOT NULL)";
                    string sql4 = "CREATE TABLE Task(id INTEGER NOT NULL PRIMARY KEY,ColumnId INTEGER NOT NULL PRIMERY KEY,email TEXT NOT NULL PRIMARY KEY,Title TEXT NOT NULL , BoardID INTEGER NOT NULL, Description TEXT NOT NULL, Column INTEGER NOT NULL,DueDate DATETIME NOT NULL,CreationTime DATETIME NOT NULL )";
                    System.Data.SQLite.SQLiteConnection.CreateFile("M3.db");
                    using (System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("data source=  M3.db"))
                    {
                        using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
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
                    }

                }
            }
            this._tableName = tableName;

        }
        public bool Update(long id, string attributeName, long attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {_tableName} set [{attributeName}]=@{attributeName} where id={id}"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }

            }
            return res > 0;
        }
        public bool Update(long id, string attributeName, string attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {_tableName} set [{attributeName}]=@{attributeName} where id={id}"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }

            }
            return res > 0;
        }
        public bool Update(long id, string attributeName, DateTime attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {_tableName} set [{attributeName}]=@{attributeName} where id={id}"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }

            }
            return res > 0;
        }

        // public abstract List<DTOs.DTO> Select(int id, string email);

        //  protected abstract DTOs.DTO ConvertReaderToObject(SQLiteDataReader reader);

        public bool Delete(DTOs.DTO DTOObj)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {_tableName} where id={DTOObj.Id}"
                };
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }

    }
}