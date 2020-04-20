using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Objects
{
    abstract class DALObject<T> where T : DALObject<T>
    {
        // takes this instance of a DAL object and returns it as a json string
        public string ToJson()
        {
            return (JsonSerializer.Serialize(this, this.GetType()));
        }

        public static T FromJson(string json)
        {
            // takes an already read json string from dal controller and returns it as an object

            return (JsonSerializer.Deserialize<T>(json));
           
        }


    }
}
