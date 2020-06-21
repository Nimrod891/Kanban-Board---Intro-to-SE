using System.Windows;


namespace WpfApp1.View
{

    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        public MainWindow()
        {
           
            InitializeComponent();
            this.DataContext = new MainViewModel();
            this.viewModel = (MainViewModel)DataContext;
        }

        private void Button_LogIn(object sender, RoutedEventArgs e)
        {
            viewModel.Login();

        }

            

        private void Button_Register(object sender, RoutedEventArgs e)
        {
            viewModel.Register();
        }

        private void Button_Registerhost(object sender, RoutedEventArgs e)
        {
            viewModel.RegisteByHost();
        }
    }
}
