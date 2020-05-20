using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    class UserDTO : DTO
    {
      //  public const string MessageUserIDColumnName = "IdUser";
        public const string MessageEmailColumnName = "email";
        public const string MessageNickNameColumnName = "NickName";
        public const string MessagePassword = "password";

      //  private long _userid;
        //public long IdUser { get => _userid; set { _userid = value; _controller.Update(Id, MessageUserIDColumnName, value); } }
        private string _email;
        public string email { get => _email; set { _email = value; _controller.Update(Id, MessageEmailColumnName, value); } }
        private string _nickname;
        public string NickName { get => _nickname; set { _nickname = value; _controller.Update(Id, MessageNickNameColumnName, value); } }
        private string _password;
        public string Password { get => _password; set { _password = value; _controller.Update(Id, MessagePassword, value); } }



        public UserDTO(long userID, string email, string nickName,long password) : base(new UserDalController())
        {
            Id = userID;
            _email = email;
            _nickname = nickName;

        }
    }