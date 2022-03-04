using RatesFixer.BLL.Models;
using RatesFixer.ViewModels.BaseViewModels;

namespace RatesFixer.ViewModels.TariffPayGroup
{
    public class TariffPayGroupDialogViewModel : DialogViewModel<TariffPayGroupVM>
    {
        public TariffPayGroupDialogViewModel(TariffPayGroupVM tariffPayGroup) : base(tariffPayGroup)
        { }
    }
}
