using RatesFixer.ViewModels.StandingOrder;
using System.Windows.Controls;

namespace RatesFixer.Views.StandingOrder
{
    /// <summary>
    /// Логика взаимодействия для StandingOrderUC.xaml
    /// </summary>
    public partial class StandingOrderUC : UserControl
    {
        public StandingOrderUC()
        {
            InitializeComponent();
        }

        public StandingOrderUC(StandingOrderViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }

    }
}
