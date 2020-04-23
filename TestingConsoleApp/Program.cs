using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TestingConsoleApp
{
    class Program
    {
        public static string GetSafeFilename(string filename)
        {
            char[] remove = {',', '.', '@', '/', ':'};
            filename=string.Join("", filename.Split(remove));
            return (string.Join("", filename.Split(Path.GetInvalidFileNameChars())));
        }

        public static void Main(string[] args)
        {
            var user = new IntroSE.Kanban.Backend.DataAccessLayer.Objects.User("nexttry@gmail.org", "asasd", 
                "malihi");
           // Console.WriteLine(user.ToJson());
            Console.ReadKey();
            //user.Save();

            Console.WriteLine(user.Import("nexttry@gmail.org").ToString());
            Console.ReadKey();
        
        }
    }
}
