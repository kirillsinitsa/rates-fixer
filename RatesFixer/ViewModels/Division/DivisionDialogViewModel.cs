using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Models;
using System.Collections.ObjectModel;
using RatesFixer.BLL.Services;
using RatesFixer.ViewModels.BaseViewModels;

namespace RatesFixer.ViewModels.Division
{
    class DivisionDialogViewModel : DialogViewModel<DivisionVM>
    {
        #region Properties
        private IFactoryFloorService factoryFloorService;
        private ObservableCollection<FactoryFloorVM> factoryFloors;
        public ObservableCollection<FactoryFloorVM> FactoryFloors
        {
            get { return factoryFloors; }
            private set
            {
                factoryFloors = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Ctors
        public DivisionDialogViewModel(DivisionVM division, string connectionString) : base(division)
        {
            factoryFloorService = new FactoryFloorService(connectionString);
            FactoryFloors = factoryFloorService.GetAll();
        }
        #endregion
    }
}
