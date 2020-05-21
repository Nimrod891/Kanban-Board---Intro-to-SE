using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    class ColumnDTO :DTO
    {
        public const string MessageLimitNumColumnName = "LimitNum";

        public const string MessageNameColumnName = "Name";
        public const string MessageNumTasksColumnName = "NumTask";

        //   private long _columnid;
        //  public long ColumnID { get => _columnid; set { _columnid = value; _controller.Update(Id, MessageColumnIDColumnName, value); } }
        private long _limitnum;
        public long LimitNum { get => _limitnum; set { _limitnum = value; _controller.Update(Id, email, MessageLimitNumColumnName, value); } }
        
        private string _name;
        public string Name { get => _name; set { _name = value; _controller.Update(Id, email, MessageNameColumnName, value); } }
        private long _numtasks;
        public long NumTasks { get => _numtasks; set { _numtasks = value; _controller.Update(Id, email, MessageNumTasksColumnName, value); } }


        public ColumnDTO(long columnID, string Email, long limitNum, string name, long numTasks) : base(new ColumnDalController())
        {
            email = Email;
            Id = columnID;
            _limitnum = limitNum;
            _name = name;
            _numtasks = numTasks;


        }


    }
}
