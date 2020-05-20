﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal abstract class DTO
    {
        public const string IDColumnName = "ID";
        protected DalController _controller;
        public long Id { get; set; } = -1;
        protected DTO(DalController controller)
        {
            _controller = controller;
        }

    }
}
