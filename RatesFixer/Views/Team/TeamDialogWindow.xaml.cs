using RatesFixer.ViewModels.Team;
using System.Windows;

namespace RatesFixer.Views.Team
{
    /// <summary>
    /// Логика взаимодействия для TeamDialogWindow.xaml
    /// </summary>
    public partial class TeamDialogWindow : Window
    {
        public TeamDialogWindow()
        {
            InitializeComponent();
        }

        public TeamDialogWindow(TeamDialogViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}
