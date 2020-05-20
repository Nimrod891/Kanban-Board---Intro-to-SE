using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal abstract class DTO
    {
        public const string IDColumnName = "id";
        protected DalController _controller;
        public const string EmailColumnName = "email";
        public int Id { get; set; } = -1;
        public string email { get; set; }


        protected DTO(DalController controller)
        {

            _controller = controller;
        }


    }
}
