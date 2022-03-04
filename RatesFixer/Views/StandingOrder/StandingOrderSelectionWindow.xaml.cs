using RatesFixer.ViewModels.StandingOrder;
using System.Windows;

namespace RatesFixer.Views.StandingOrder
{
    /// <summary>
    /// Логика взаимодействия для StandingOrderSelectionWindow.xaml
    /// </summary>
    public partial class StandingOrderSelectionWindow : Window
    {
        public StandingOrderSelectionWindow()
        {
            InitializeComponent();
        }

        public StandingOrderSelectionWindow(StandingOrderSelectionViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}
