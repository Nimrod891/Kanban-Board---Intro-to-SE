using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WpfApp1.Model;
using WpfApp1.View;

namespace WpfApp1.View
{
    class MainViewModel : NotifableObject
    {
        public BackendController Controller { get; private set; }
        private string _regemail = "";
        private string _regpassword = "";
         private string _nickname = "";
        private string _username = "";
        private string _password = "";

        public string Username
        {
            get => _username;
            set
            {
                this._username = value;
                RaisePropertyChanged("Username");
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                this._password = value;
                RaisePropertyChanged("Password");
            }
        }
        public string NickName
        {
            get => _nickname;
            set
            {
                this._nickname = value;
                RaisePropertyChanged("NickName");
            }
        }
        public string RegPassword
        {
            get => _regpassword;
            set
            {
                this._regpassword = value;
                RaisePropertyChanged("RegPassword");
            }
        }
        public string RegEmail
        {
            get => _regemail;
            set
            {
                this._regemail= value;
                RaisePropertyChanged("RegEmail");
            }
        }
        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                this._message = value;
                RaisePropertyChanged("Message");
            }
        }

        public void Login()
        {
            Message = "";
            try
            {
               Controller.Login(Username, Password);
                RegEmail = "";
                RegPassword = "";
                NickName = "";
                Password = "";
                Username = "";
                var board = new BoardWindow(Username, Controller.Service);
                board.ShowDialog();

            }
            catch (Exception e)
            {
                MessageBox.Show("orforfkroffrf"+e.Message);
            }

        }
        public Brush LoginBrush
        {
            get { return string.IsNullOrWhiteSpace(Password) ? Brushes.Red : Brushes.Gray; }
        }
        public void Register()
        {
            Message = "";
            try
            {
                Controller.Register(RegEmail,RegPassword,NickName);
                RegEmail = "";
                RegPassword = "";
                NickName = "";
                Password = "";
                Username = "";
                MessageBox.Show("Register was done Succesfulli");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public MainViewModel()
        {
            this.Controller = new BackendController();
        }
    }
}
