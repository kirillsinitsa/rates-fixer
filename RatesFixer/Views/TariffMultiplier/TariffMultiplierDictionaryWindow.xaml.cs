using RatesFixer.ViewModels.TariffMultiplier;
using System;
using System.Windows;

namespace RatesFixer.Views.TariffMultiplier
{
    /// <summary>
    /// Логика взаимодействия для TariffMultiplierDictionaryWindow.xaml
    /// </summary>
    public partial class TariffMultiplierDictionaryWindow : Window
    {
        public TariffMultiplierDictionaryWindow()
        {
            InitializeComponent();
        }
        public TariffMultiplierDictionaryWindow(TariffMultiplierDictionaryViewModel viewModel) : this()
        {
            DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(() => this.Close());
        }
    }
}
