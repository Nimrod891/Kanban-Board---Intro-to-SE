using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class DALController
    {
        public static void Write(string filename, string content)
        {
            File.WriteAllText(filename, content);
        }

        // recieves the NAME of the required file and returns its CONTENT as a string if file exists
        // else, creates an empty file of the same name.
        public static string Read(string filename)
        {
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
