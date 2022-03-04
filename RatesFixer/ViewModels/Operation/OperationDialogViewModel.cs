using RatesFixer.BLL.Models;
using RatesFixer.ViewModels.BaseViewModels;

namespace RatesFixer.ViewModels.Operation
{
    public class OperationDialogViewModel : DialogViewModel<OperationVM>
    {
        public OperationDialogViewModel(OperationVM operation) : base(operation)
        { }
    }
}
