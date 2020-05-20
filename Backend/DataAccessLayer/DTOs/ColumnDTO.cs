using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    class ColumnDTO :DTO
    {
      //  public const string MessageColumnIDColumnName = "ColumnID";
        public const string MessageLimitNumColumnName = "LimitNum";
        public const string MessageBoardIDColumnName = "BoardID";
        public const string MessageNameColumnName = "Name";
        public const string MessageNumTasksColumnName = "NumTasks";

     //   private long _columnid;
      //  public long ColumnID { get => _columnid; set { _columnid = value; _controller.Update(Id, MessageColumnIDColumnName, value); } }
        private long _limitnum;
        public long LimitNum { get => _limitnum; set { _limitnum = value; _controller.Update(Id, MessageLimitNumColumnName, value); } }
        private long _boardid;
        public long BoardID { get => _boardid; set { _boardid = value; _controller.Update(Id, MessageBoardIDColumnName, value); } }
        private string _name;
        public string Name { get => _name; set { _name = value; _controller.Update(Id, MessageNameColumnName, value); } }
        private long _numtasks;
        public long NumTasks { get => _numtasks; set { _numtasks = value; _controller.Update(Id, MessageNumTasksColumnName, value); } }


        public ColumnDTO(long columnID, long limitNum ,long BoardID ,string name, long numTasks) : base(new ColumnDalController())
        {
            Id = columnID;
            _limitnum = limitNum;
            _boardid = BoardID;
            _name = name;
            _numtasks = numTasks;


        }

    }
}
