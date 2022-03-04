using RatesFixer.BLL.Models;
using RatesFixer.ViewModels.BaseViewModels;

namespace RatesFixer.ViewModels.FactoryFloor
{
    public class FactoryFloorDialogViewModel : DialogViewModel<FactoryFloorVM>
    {
        public FactoryFloorDialogViewModel(FactoryFloorVM factoryFloor) : base(factoryFloor)
        { }
    }
}
