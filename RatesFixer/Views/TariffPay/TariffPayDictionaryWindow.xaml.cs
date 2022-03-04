using RatesFixer.ViewModels.TariffPay;
using System;
using System.Windows;

namespace RatesFixer.Views.TariffPay
{
    /// <summary>
    /// Логика взаимодействия для TariffPayDictionaryWindow.xaml
    /// </summary>
    public partial class TariffPayDictionaryWindow : Window
    {
        public TariffPayDictionaryWindow()
        {
            InitializeComponent();
        }
        public TariffPayDictionaryWindow(TariffPayDictionaryViewModel viewModel) : this()
        {
            DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(() => this.Close());
        }
    }
}
