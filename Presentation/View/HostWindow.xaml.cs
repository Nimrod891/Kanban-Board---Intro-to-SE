using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace Presentation.View
{
    /// <summary>
    /// Interaction logic for HostWindow.xaml
    /// </summary>
    public partial class HostWindow : Window
    {
        MainViewModel _vm;
        public HostWindow(IService service)
        {
            InitializeComponent();
           // _vm = new MainViewModel(service);
            this.DataContext = new MainViewModel(service);
            this._vm = (MainViewModel)DataContext;
        }

        private void Button_Registerhost(object sender, RoutedEventArgs e)
        {
            _vm.RegisterbyHost();
            this.Close();
        }

        private void Button_Register(object sender, RoutedEventArgs e)
        {
            _vm.Register();
            this.Close();
        }
    }
}
