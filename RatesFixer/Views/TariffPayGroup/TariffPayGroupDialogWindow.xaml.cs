using RatesFixer.BLL.Models;
using RatesFixer.ViewModels.BaseViewModels;
using System.Windows;

namespace RatesFixer.Views.TariffPayGroup
{
    /// <summary>
    /// Логика взаимодействия для TariffPayGroupDialogWindow.xaml
    /// </summary>
    public partial class TariffPayGroupDialogWindow : Window
    {
        public TariffPayGroupDialogWindow()
        {
            InitializeComponent();
        }
        public TariffPayGroupDialogWindow(DialogViewModel<TariffPayGroupVM> viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}

