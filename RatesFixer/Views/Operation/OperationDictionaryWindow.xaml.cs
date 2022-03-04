using RatesFixer.ViewModels.Operation;
using System;
using System.Windows;

namespace RatesFixer.Views.Operation
{
    /// <summary>
    /// Логика взаимодействия для OperationDictionaryWindow.xaml
    /// </summary>
    public partial class OperationDictionaryWindow : Window
    {
        public OperationDictionaryWindow()
        {
            InitializeComponent();
        }
        public OperationDictionaryWindow(OperationDictionaryViewModel viewModel) : this()
        {
            DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(() => this.Close());
        }
    }
}