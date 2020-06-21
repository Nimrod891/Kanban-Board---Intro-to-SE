using System.Windows;
using WpfApp1.ViewModel;
using IntroSE.Kanban.Backend.ServiceLayer;
namespace WpfApp1.View
{
   
    /// <summary>
    /// Interaction logic for BoardWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        private BoardViewModel vm;
        public BoardWindow(string email,IService service)
        {
            InitializeComponent();
            vm = new BoardViewModel(new BackendController(service), email);
            this.DataContext = vm;
        }

        private void logout_button(object sender, RoutedEventArgs e)
        {

            vm.LogOut(vm.Username);
            this.Close();
        }

        

        private void move_right_button(object sender, RoutedEventArgs e)
        {
            vm.MoveColumRight(vm.SelectedColumn.ColumnOrdianl);
        }

        private void view_column_button(object sender, RoutedEventArgs e)
        {
            vm.GetColumn();
        }

        private void move_left_button(object sender, RoutedEventArgs e)
        {
            vm.MoveColumnLeft(vm.SelectedColumn.ColumnOrdianl);
        }

        private void set_limit_button(object sender, RoutedEventArgs e)
        {
            vm.SetLimitNum(vm.Username, vm.SelectedColumn.ColumnOrdianl, vm.NewLimitNum);
        }

        private void remove_column_button(object sender, RoutedEventArgs e)
        {
            vm.RemoveColumn(vm.SelectedColumn.ColumnOrdianl);
        }

        private void add_new_task_button(object sender, RoutedEventArgs e)
        {
            vm.AddTask();
        }

        private void add_column_button(object sender, RoutedEventArgs e)
        {
            vm.AddColumn();
        }

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
