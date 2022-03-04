using RatesFixer.ViewModels.Team;
using System;
using System.Windows;

namespace RatesFixer.Views.Team
{
    /// <summary>
    /// Логика взаимодействия для TeamDictionaryWindow.xaml
    /// </summary>
    public partial class TeamDictionaryWindow : Window
    {
        public TeamDictionaryWindow()
        {
            InitializeComponent();
        }

        public TeamDictionaryWindow(TeamDictionaryViewModel viewModel) : this()
        {
            DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(() => this.Close());
        }
    }
}
