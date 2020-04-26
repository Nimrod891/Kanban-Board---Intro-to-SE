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
            
            

            var myService = new IntroSE.Kanban.Backend.ServiceLayer.Service();
            myService.Register("nana@baka.co", "Idodo1", "Idojssdo");
            myService.Register("hila@hg..nbb", "Idodo1", "Hilablabla");
           
            myService.Login("test@test.test", "Test1");

            myService.AddTask("test@test.test", "",
               "gel", DateTime.Parse("13/1/2026"));



            //Console.WriteLine("Click to continue2");
            //Console.ReadKey();
            //myService.UpdateTaskDescription("test@test.test", 0, 4, "new title update");

            //Console.WriteLine("Click to continue3");
            //Console.ReadKey();
            //myService.UpdateTaskDueDate("test@test.test", 0, 5, DateTime.Parse("27/4/2020"));
            //Console.WriteLine("Click to continue4");
            //Console.ReadKey();

            //Console.WriteLine("trying to get backlog: "+ myService.GetColumn("test@test.test", "backlog").);
            //IntroSE.Kanban.Backend.ServiceLayer.Response<IntroSE.Kanban.Backend.ServiceLayer.Column> checkColumn = myService.GetColumn
            //    ("test@test.test", "backlog");
            //Console.WriteLine(checkColumn.Value);
            Console.WriteLine("Click to continue4");
            Console.ReadKey();
            /* var user = new IntroSE.Kanban.Backend.DataAccessLayer.Objects.User("nexttry@gmail.org", "asasd",
                 "malihi");
             Console.WriteLine(user.ToJson());
             Console.WriteLine("Click to continue1");
             Console.ReadKey();
             user.Save();

             Console.WriteLine(user.Import("nexttry@gmail.org").ToString());
             Console.WriteLine("Click to continue2");
             Console.ReadKey();
             var board1 = new IntroSE.Kanban.Backend.DataAccessLayer.Objects.Board("iso34@goog.cv");
             var board2 = new IntroSE.Kanban.Backend.DataAccessLayer.Objects.Board("buddc@nana.net");

             Console.WriteLine(board1.ToJson());
             Console.WriteLine(board2.ToJson());
             Console.WriteLine("Click to continue3");
             Console.ReadKey();
             board1.Save();
             board2.Save();

             Console.WriteLine(board1.Import("iso34@goog.cv").ToString());
             Console.WriteLine("click to continue4");
             Console.ReadKey();
             var dalcontrol = new IntroSE.Kanban.Backend.DataAccessLayer.DALController();
             Console.WriteLine("click to continue5");
             Console.ReadKey();
             Console.WriteLine(dalcontrol.Boards["iso34@goog.cv"].ToString());
             Console.WriteLine("click to continue6");
             Console.ReadKey();*/

        }
    }
}
