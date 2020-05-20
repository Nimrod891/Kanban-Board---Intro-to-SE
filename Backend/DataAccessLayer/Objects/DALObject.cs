using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using System.Text.Json;
using Newtonsoft.Json;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Objects
{ }
   /* public abstract class DALObject<T> where T : DALObject<T>
    {
        // takes this instance of a DAL object and returns it as a json string
        public string ToJson()
        {
            //return (JsonSerializer.Serialize(this, this.GetType()));
            return (JsonConvert.SerializeObject(this,Formatting.Indented));

        }

        public static T FromJson(string json)
        {
            // takes an already read json string from dal controller and returns it as an object

            //return (JsonSerializer.Deserialize<T>(json));
            json = DALController.Read(json);
            return (JsonConvert.DeserializeObject<T>(json));


           
        }

        public abstract void Save();

        public string GetSafeFilename(string filename)
        {
            char[] remove = { ',', '.', '@', '/', ':' };
            filename = string.Join("", filename.Split(remove));
            return (string.Join("", filename.Split(Path.GetInvalidFileNameChars())));
        }


    }
}
*/

