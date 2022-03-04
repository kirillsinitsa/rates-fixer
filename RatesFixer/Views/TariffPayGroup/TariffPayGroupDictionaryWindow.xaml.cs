using RatesFixer.ViewModels.TariffPayGroup;
using System;
using System.Windows;

namespace RatesFixer.Views.TariffPayGroup
{
    /// <summary>
    /// Логика взаимодействия для TariffPayGroupDictionaryWindow.xaml
    /// </summary>
    public partial class TariffPayGroupDictionaryWindow : Window
    {
        public TariffPayGroupDictionaryWindow()
        {
            InitializeComponent();
        }
        public TariffPayGroupDictionaryWindow(TariffPayGroupDictionaryViewModel viewModel) : this()
        {
            DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(() => this.Close());
        }
    }
}
