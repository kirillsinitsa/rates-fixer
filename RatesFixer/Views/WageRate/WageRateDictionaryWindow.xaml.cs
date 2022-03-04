using RatesFixer.ViewModels.WageRate;
using System;
using System.Windows;

namespace RatesFixer.Views.WageRate
{
    /// <summary>
    /// Логика взаимодействия для WageRateDictionaryWindow.xaml
    /// </summary>
    public partial class WageRateDictionaryWindow : Window
    {
        public WageRateDictionaryWindow()
        {
            InitializeComponent();
        }
        public WageRateDictionaryWindow(WageRateDictionaryViewModel viewModel) : this()
        {
            DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(() => this.Close());
        }
    }
}
