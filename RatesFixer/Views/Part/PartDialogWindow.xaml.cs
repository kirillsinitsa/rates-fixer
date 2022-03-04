using RatesFixer.BLL.Models;
using RatesFixer.ViewModels.BaseViewModels;
using System.Windows;

namespace RatesFixer.Views.Part
{
    /// <summary>
    /// Логика взаимодействия для PartDialogWindow.xaml
    /// </summary>
    public partial class PartDialogWindow : Window
    {
        public PartDialogWindow()
        {
            InitializeComponent();
        }
        public PartDialogWindow(DialogViewModel<PartVM> viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}
