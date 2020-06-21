using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    class UserModel : NotifableModelObject
    {
        private string _email;
        public string getUseremail()
        {
            return _email;
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged("Email");
            }
        }
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
              //  RaisePropertyChanged("Email");
            }
        }
        private string _nickname;

        public string Nickname
        {
            get => _nickname;
            set
            {
                _nickname = value;
                RaisePropertyChanged("Nickname");
            }
        }
        public BoardModel Board()
        {
            return new BoardModel(Controller, Email);
        }

        public UserModel(BackendController controller, string email) : base(controller)
        {
            this.Email = email;
        }
    }
}
