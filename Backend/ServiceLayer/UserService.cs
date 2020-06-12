using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    class userService
    {
        BusinessLayer.UserPackage.UserController myUserContorller;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public userService()
        {
            this.myUserContorller = new BusinessLayer.UserPackage.UserController();
        }
        public Response Register(string email, string password, string nickname)
        {
            try
            {
                myUserContorller.Register(email, password, nickname);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }


        public Response<User> Login(string email, string password)
        {
            try
            {
                BusinessLayer.UserPackage.User u = myUserContorller.Login(email, password);
                User serviceUser = new User(u.GetEmail(), u.GetNickname());
                log.Info("User " + email + " has logged in");
                return new Response<User>(serviceUser);
            }
            catch (Exception e)
            {
                log.Error("User "+email+" threw exception during login");
                return new Response<User>(e.Message);
            }
        }

        public Response Logout(string email)
        {
            try
            {
                myUserContorller.Logout(email);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        public Response DeleteData()
        {
            try
            {
                myUserContorller.DeleteData();
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
            
        }
    }
}
