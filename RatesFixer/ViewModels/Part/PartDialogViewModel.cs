using Prism.Events;
using RatesFixer.BLL.Models;
using RatesFixer.ViewModels.BaseViewModels;
using RatesFixer.ViewModels.PartOperation;

namespace RatesFixer.ViewModels.Part
{
    public class PartDialogViewModel : DialogViewModel<PartVM>
    {
        private PartOperationsDictionaryViewModel partOperationsViewModel;
        public PartOperationsDictionaryViewModel PartOperationsVM => partOperationsViewModel;
        public PartDialogViewModel(IEventAggregator eventAggregator, string connectionString, PartVM part) : base(part)
        {
            partOperationsViewModel = new PartOperationsDictionaryViewModel(eventAggregator, connectionString, CurrentItem.PartId);
        }
    }
}
