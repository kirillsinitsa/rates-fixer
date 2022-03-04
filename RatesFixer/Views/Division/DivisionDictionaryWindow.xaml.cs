using RatesFixer.ViewModels.Division;
using System;
using System.Windows;

namespace RatesFixer.Views.Division
{
    /// <summary>
    /// Логика взаимодействия для DivisionDictionaryWindow.xaml
    /// </summary>
    public partial class DivisionDictionaryWindow : Window
    {
        public DivisionDictionaryWindow()
        {
            InitializeComponent();
        }
        public DivisionDictionaryWindow(DivisionDictionaryViewModel viewModel) : this()
        {
            DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(() => this.Close());
        }

    }
}
