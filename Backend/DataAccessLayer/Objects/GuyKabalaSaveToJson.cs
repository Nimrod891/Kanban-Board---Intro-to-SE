using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Objects
{
    class GuyKabalaSaveToJson
    {
        public string id { get; set; }
        public string name { get; set; }

        public GuyKabalaSaveToJson(string id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public void tojson()
        {
            string objectAsJason = JsonSerializer.Serialize(this, this.GetType());
            File.WriteAllText("testfile.json", objectAsJason);
        
        }

        public void readingFromJson()
        {
            GuyKabalaSaveToJson a = new GuyKabalaSaveToJson("justID", "justNAME");
            a.tojson(); // writes to the json file

            if (File.Exists("testfile.json"))
            {
                string objectAsJson = File.ReadAllText("testfile.json");
                a = JsonSerializer.Deserialize<GuyKabalaSaveToJson>(objectAsJson);
            }
            else
            {
                //create empty userController
            }
            

        }

    }
}
