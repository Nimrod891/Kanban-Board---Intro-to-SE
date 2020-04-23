using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Objects
{
    class Column : DALObject<Column>
    {
        private string name;
        private int columnId;
        private int limitNum;
        private int numOfTasks;
        private Dictionary<int, Task> tasks;


        public Column(string name, int columnId)
        {
            this.name = name;
            this.columnId = columnId;
            limitNum = -1;
            numOfTasks = 0;
            tasks = new Dictionary<int, Task>();
           
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}
