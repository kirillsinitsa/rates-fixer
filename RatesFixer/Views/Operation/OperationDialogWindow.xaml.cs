using RatesFixer.BLL.Models;
using RatesFixer.ViewModels.BaseViewModels;
using System.Windows;

namespace RatesFixer.Views.Operation
{
    /// <summary>
    /// Логика взаимодействия для OperationDialogWindow.xaml
    /// </summary>
    public partial class OperationDialogWindow : Window
    {
        public OperationDialogWindow()
        {
            InitializeComponent();
        }

        public OperationDialogWindow(DialogViewModel<OperationVM> viewModel) : this()
        {
            DataContext = viewModel;
        }

    }
}
