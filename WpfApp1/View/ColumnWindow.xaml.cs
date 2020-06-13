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
using WpfApp1.Model;
using WpfApp1.ViewModel;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for ColumnWindow.xaml
    /// </summary>
    public partial class ColumnWindow : Window
    {
        private ColumnViewModel vm;

     //   private string _email="";
       // private int _columnordinal;
        //private List<TaskModel> tasks;
        
         
                public ColumnWindow(IService thisService,  string email, int columnOrdinal)
        {


            InitializeComponent();
            vm = new ColumnViewModel(new BackendController(thisService), email, columnOrdinal);
            this.DataContext = vm;
            
        }

        private void return_to_board_button(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void advance_button(object sender, RoutedEventArgs e)
        {

        }

        private void view_task_button(object sender, RoutedEventArgs e)
        {
            vm.GetTask();
        }


    }
}
