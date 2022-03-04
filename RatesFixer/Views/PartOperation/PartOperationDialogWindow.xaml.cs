using RatesFixer.BLL.Models;
using RatesFixer.ViewModels.BaseViewModels;
using System.Windows;

namespace RatesFixer.Views.PartOperation
{
    /// <summary>
    /// Логика взаимодействия для PartOperationDialogWindow.xaml
    /// </summary>
    public partial class PartOperationDialogWindow : Window
    {
        public PartOperationDialogWindow()
        {
            InitializeComponent();
        }

        public PartOperationDialogWindow(DialogViewModel<PartOperationVM> viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}
