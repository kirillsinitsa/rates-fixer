using RatesFixer.BLL.Models;
using RatesFixer.ViewModels.BaseViewModels;
using System.Windows;

namespace RatesFixer.Views.TariffMultiplier
{
    /// <summary>
    /// Логика взаимодействия для TariffMultiplierDialogWindow.xaml
    /// </summary>
    public partial class TariffMultiplierDialogWindow : Window
    {
        public TariffMultiplierDialogWindow()
        {
            InitializeComponent();
        }
        public TariffMultiplierDialogWindow(DialogViewModel<TariffMultiplierVM> viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}
