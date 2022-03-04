using RatesFixer.ViewModels.WorkStation;
using System;
using System.Windows;

namespace RatesFixer.Views.WorkStation
{
    /// <summary>
    /// Логика взаимодействия для WorkStationDictionaryWindow.xaml
    /// </summary>
    public partial class WorkStationDictionaryWindow : Window
    {
        public WorkStationDictionaryWindow()
        {
            InitializeComponent();
        }
        public WorkStationDictionaryWindow(WorkStationDictionaryViewModel viewModel) : this()
        {
            DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(() => this.Close());
        }
    }
}
