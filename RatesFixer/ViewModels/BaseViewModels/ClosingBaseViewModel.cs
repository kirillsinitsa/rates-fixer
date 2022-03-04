using Prism.Events;
using System;

namespace RatesFixer.ViewModels.BaseViewModels
{
    public abstract class ClosingBaseViewModel : BaseViewModel
    {
        protected readonly IEventAggregator eventAggregator;
        public Action CloseAction { get; set; }

        public void Close()
        {
            CloseAction();
        }

        public ClosingBaseViewModel(IEventAggregator eventAggregator) : base()
        {
            if (eventAggregator != null)
            {
                this.eventAggregator = eventAggregator;
                this.eventAggregator.GetEvent<ApplicationClosedEvent>().Subscribe(Close);
            }
        }
    }
}
