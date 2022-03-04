using RatesFixer.ViewModels.FactoryFloor;
using System;
using System.Windows;

namespace RatesFixer.Views.FactoryFloor
{
    /// <summary>
    /// Логика взаимодействия для FactoryFloorDictionaryWindow.xaml
    /// </summary>
    public partial class FactoryFloorDictionaryWindow : Window
    {
        public FactoryFloorDictionaryWindow()
        {
            InitializeComponent();
        }

        public FactoryFloorDictionaryWindow(FactoryFloorDictionaryViewModel viewModel) : this()
        {
            DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(() => this.Close());
        }

    }
}
