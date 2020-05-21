using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal class BoardDTO : DTO
    {
        public BoardDTO(long ID, string Email) : base(new BoardDalController())
        {
            email = Email;
            Id = ID;
        }

    }
}
