using Prism.Events;

namespace RatesFixer.Infrastructure
{
    internal sealed class ApplicationService
    {
        private ApplicationService() { }

        private static readonly ApplicationService _instance = new ApplicationService();

        internal static ApplicationService Instance { get { return _instance; } }

        private IEventAggregator eventAggregator;
        internal IEventAggregator EventAggregator
        {
            get
            {
                if (eventAggregator == null)
                    eventAggregator = new EventAggregator();

                return eventAggregator;
            }
        }
    }
}
