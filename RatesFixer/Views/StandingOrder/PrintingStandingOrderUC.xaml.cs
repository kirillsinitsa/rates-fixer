using RatesFixer.ViewModels.StandingOrder;
using System.Windows.Controls;

namespace RatesFixer.Views.StandingOrder
{
    /// <summary>
    /// Логика взаимодействия для PrintingStandingOrderUC.xaml
    /// </summary>
    public partial class PrintingStandingOrderUC : UserControl
    {
        public PrintingStandingOrderUC()
        {
            InitializeComponent();
        }

        public PrintingStandingOrderUC(PrintingStandingOrderViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}
