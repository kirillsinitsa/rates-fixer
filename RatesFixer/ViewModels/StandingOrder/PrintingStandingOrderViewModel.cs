using Prism.Events;
using RatesFixer.BLL.Models;
using RatesFixer.ViewModels.BaseViewModels;
using RatesFixer.Views.StandingOrder;
using System.Dynamic;

namespace RatesFixer.ViewModels.StandingOrder
{
    public class PrintingStandingOrderViewModel : BaseViewModel
    {
        private bool toPrint = false;
        public bool ToPrint
        {
            get => toPrint;
            set
            {
                toPrint = value;
                OnPropertyChanged();
            }
        }

        private StandingOrderUC standingOrder;
        public StandingOrderUC StandingOrder
        {
            get => standingOrder;
            set
            {
                standingOrder = value;
                OnPropertyChanged();
            }
        }

        public PrintingStandingOrderViewModel(StandingOrderUC standingOrder)
        {
            this.standingOrder = standingOrder;
        }
    }
}
