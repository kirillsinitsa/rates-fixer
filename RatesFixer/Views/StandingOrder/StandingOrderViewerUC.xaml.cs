using RatesFixer.ViewModels.StandingOrder;
using System.Windows;
using System.Windows.Controls;

namespace RatesFixer.Views.StandingOrder
{
    /// <summary>
    /// Логика взаимодействия для StandingOrderViewerUC.xaml
    /// </summary>
    public partial class StandingOrderViewerUC : UserControl
    {
        public StandingOrderViewerUC()
        {
            InitializeComponent();
        }

        public StandingOrderViewerUC(StandingOrderViewerViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}
