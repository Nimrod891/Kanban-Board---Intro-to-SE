using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public struct Board
    {
        public readonly IReadOnlyCollection<string> ColumnsNames;
        //public readonly string emailCreator; // add to signature as well
        internal Board(IReadOnlyCollection<string> columnsNames) 
        {
            this.ColumnsNames = columnsNames;
            //his.emailCreator = emailCreator;
        }
        // You can add code here
    }
}
