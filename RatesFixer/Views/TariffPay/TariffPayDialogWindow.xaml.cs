using RatesFixer.ViewModels.TariffPay;
using System.Windows;

namespace RatesFixer.Views.TariffPay
{
    /// <summary>
    /// Логика взаимодействия для TariffPayDialogWindow.xaml
    /// </summary>
    public partial class TariffPayDialogWindow : Window
    {
        public TariffPayDialogWindow()
        {
            InitializeComponent();
        }

        public TariffPayDialogWindow(TariffPayDialogViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}
