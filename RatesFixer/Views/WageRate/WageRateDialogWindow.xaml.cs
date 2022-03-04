using RatesFixer.ViewModels.WageRate;
using System.Windows;

namespace RatesFixer.Views.WageRate
{
    /// <summary>
    /// Логика взаимодействия для WageRateDialogWindow.xaml
    /// </summary>
    public partial class WageRateDialogWindow : Window
    {
        public WageRateDialogWindow()
        {
            InitializeComponent();
        }
        public WageRateDialogWindow(WageRateDialogViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}

