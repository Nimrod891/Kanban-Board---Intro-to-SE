using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer.Objects;
using Practical_DB.DataAccessLayer.DTOs;
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
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "M3.db"));
            this._connectionString = $"Data Source={path}; Version=3;";
            this._tableName = tableName;
        }
        public List<BusinessLayer.BoardPackage.Task> GetTasksbyID(int idColumn)
        {
            List<DTOs.TaskDTO> TasksbyID = 
            TaskDalController TASKI = new TaskDalController();
            TasksbyID = TASKI.SelectAllTasks();
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

        protected List<DTOs.DTO> Select()
        {
            List<DTOs.DTO> results = new List<DTOs.DTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {_tableName};";
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

        protected abstract DTOs.DTO ConvertReaderToObject(SQLiteDataReader reader);

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
        public static string Read(string filename)
        {

            while(DataR)
        }
    }
























    
        /*
        public Dictionary<string, Board> Boards {get;}

        public DALController()
        {
            //string boardsJson = Read("Boards.json");
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Kanban JSON Files", "Boards");
            this.Boards = new Dictionary<string, Board>();
            Directory.CreateDirectory(path);
                foreach (string file in Directory.EnumerateFiles(path, "*.json"))
                {
                    Board boardToAdd = new Board();
                    boardToAdd = Board.FromJson(Read(file));
                    Boards.Add(boardToAdd.email, boardToAdd);

                    /// if boards exist in the folder /Kanban JSON Files/Boards 
                    /// this will create a dictionary of {email, board} as a field in DALController

                }
            
            
        }

        public static void Write(string filename, string content)
        {
            
            string dir = Directory.GetCurrentDirectory();
            dir = Path.Combine(dir, "Kanban JSON Files");

            //Path.GetFileName(filename);
            //Console.WriteLine(dir);
             Directory.CreateDirectory(dir.Replace((Path.GetFileName(filename)), ""));
            //(Path.GetFileName(filename)).ToCharArray()
             //   );
            //Console.WriteLine(dir);
            string f = Path.Combine(dir, filename);
            File.WriteAllText(f, content);
        }

        // recieves the NAME of the required file and returns its CONTENT as a string if file exists
        // else, creates an empty file of the same name.
        public static string Read(string filename)
        {
            filename = Path.Combine(Directory.GetCurrentDirectory(), "Kanban JSON Files", filename);
            if (File.Exists(filename))
            {
                return (File.ReadAllText(filename));   
            }
            else
            {
                throw new NotImplementedException();
                //create empty file
            }
            /*
        }
    }
}
