using RatesFixer.ViewModels.Employee;
using System.Windows;

namespace RatesFixer.Views.Employee
{
    /// <summary>
    /// Логика взаимодействия для EmployeeDialogWindow.xaml
    /// </summary>
    public partial class EmployeeDialogWindow : Window
    {
        public EmployeeDialogWindow()
        {
            InitializeComponent();
        }

        public EmployeeDialogWindow(EmployeeDialogViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }

    }
}
