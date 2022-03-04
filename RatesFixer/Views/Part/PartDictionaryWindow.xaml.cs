using RatesFixer.ViewModels.Part;
using System;
using System.Windows;

namespace RatesFixer.Views.Part
{
    /// <summary>
    /// Логика взаимодействия для PartDictionaryWindow.xaml
    /// </summary>
    public partial class PartDictionaryWindow : Window
    {
        public PartDictionaryWindow()
        {
            InitializeComponent();
        }
        public PartDictionaryWindow(PartDictionaryViewModel viewModel) : this()
        {
            DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(() => this.Close());
        }
    }
}
