using RatesFixer.BLL.Models;
using RatesFixer.ViewModels.BaseViewModels;
using System.Windows;

namespace RatesFixer.Views.Division
{
    /// <summary>
    /// Логика взаимодействия для DivisionDialogWindow.xaml
    /// </summary>
    public partial class DivisionDialogWindow : Window
    {
        public DivisionDialogWindow()
        {
            InitializeComponent();
        }
        public DivisionDialogWindow(DialogViewModel<DivisionVM> viewModel) : this()
        {
            DataContext = viewModel;
        }

    }
}
