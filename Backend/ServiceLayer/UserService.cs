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

        public userService()
        {
            this.myUserContorller = new BusinessLayer.UserPackage.UserController();
        }
        public Response Register(string email, string password, string nickname)

        {
            try
            {
                myUserContorller.Register(email, password, nickname);

            }
            catch (Exception e)
            {
                return new Response<object>(e.Message);
            }
            return new Response();
        }


        public Response<User> Login(string email, string password)
        {
            User u = new User();
            try
            {
                myUserContorller.Login(email, password);

            }
            catch (Exception e)
            {
                return new Response<User>(u, e.Message);
            }
            return new Response<User>(u);

        }

        public Response Logout(string email)
        {

            try
            {
                myUserContorller.Logout(email);

            }
            catch (Exception e)
            {
                return new Response<object>(e);
            }
            return new Response();

        }
        public Response LoadData()
        {

        }
    }
