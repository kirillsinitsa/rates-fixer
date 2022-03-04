using RatesFixer.ViewModels.Employee;
using System;
using System.Windows;

namespace RatesFixer.Views.Employee
{
    /// <summary>
    /// Логика взаимодействия для EmployeeDictionaryWindow.xaml
    /// </summary>
    public partial class EmployeeDictionaryWindow : Window
    {
        public EmployeeDictionaryWindow()
        {
            InitializeComponent();
        }

        public EmployeeDictionaryWindow(EmployeeDictionaryViewModel viewModel) : this()
        {
            DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(() => this.Close());
        }

    }
}
