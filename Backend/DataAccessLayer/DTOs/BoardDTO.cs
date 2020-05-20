using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal class BoardDTO : DTO
    {
        public const string BoardNameColumnName = "Name";


        private string _name;
        public string Name { get => _name; set { _name = value; _controller.Update(Id, BoardNameColumnName, value); } }

        public BoardDTO(long ID, string Title,string email) : base(new BoardDalController())
        {
            Email = email;
            Id = ID;
            _name = Title;
        }
    }
}
