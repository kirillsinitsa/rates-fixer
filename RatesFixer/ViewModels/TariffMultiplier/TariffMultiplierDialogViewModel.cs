using RatesFixer.BLL.Models;
using RatesFixer.ViewModels.BaseViewModels;

namespace RatesFixer.ViewModels.TariffMultiplier
{
    public class TariffMultiplierDialogViewModel : DialogViewModel<TariffMultiplierVM>
    {
        public TariffMultiplierDialogViewModel(TariffMultiplierVM tariffMultiplier) : base(tariffMultiplier)
        { }
    }
}
