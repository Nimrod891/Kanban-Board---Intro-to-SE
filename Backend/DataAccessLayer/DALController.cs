using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer.Objects;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class DALController
    {
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
            Directory.CreateDirectory(dir.TrimEnd((Path.GetFileName(filename)).ToCharArray()));
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
            
        }
    }
}
