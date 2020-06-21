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
using Presentation.ViewModel;

namespace Presentation.View
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        private TaskViewModel vm;

        public TaskWindow(string email, IService service,int columnordinal1,int idtask)
        {
            InitializeComponent();
            BackendController back = new BackendController(service);
            vm = new TaskViewModel(back,email, columnordinal1, back.GetColumn(email,columnordinal1).Value.Tasks.ToList<IntroSE.Kanban.Backend.ServiceLayer.Task>()[idtask]);
            this.DataContext = vm;


        }
        private void ChangeDescriptionButton(object sender, RoutedEventArgs e)
        {
            vm.UpdateTaskDiscription(vm.Description);
        }

        private void Change_due_date_button(object sender, RoutedEventArgs e)
        {
            vm.UpdateTaskDueDate(vm.DueDate);
        }

        private void Change_TitleButton(object sender, RoutedEventArgs e)
        {
            vm.UpdateTaskTitle();

        }

        private void Return_Button(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
