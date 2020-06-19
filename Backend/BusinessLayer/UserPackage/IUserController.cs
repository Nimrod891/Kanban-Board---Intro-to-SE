using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserPackage
{
    interface IUserController
    {
        void addUser(User u);
        bool Login(User u);
    }
}
