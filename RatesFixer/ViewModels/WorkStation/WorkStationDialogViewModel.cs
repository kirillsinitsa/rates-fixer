using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Models;
using System.Collections.ObjectModel;
using RatesFixer.BLL.Services;
using RatesFixer.Infrastructure;
using RatesFixer.ViewModels.BaseViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace RatesFixer.ViewModels.WorkStation
{
    class WorkStationDialogViewModel : DialogViewModel<WorkStationVM>
    {
        #region Properties
        private readonly IFactoryFloorService factoryFloorService;
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

        private FactoryFloorVM selectedFactoryFloor;
        public FactoryFloorVM SelectedFactoryFloor
        {
            get { return selectedFactoryFloor; }
            set
            {
                selectedFactoryFloor = value;
                OnPropertyChanged();
            }
        }

        private readonly IDivisionService divisionService;
        private ObservableCollection<DivisionVM> divisions;
        public ObservableCollection<DivisionVM> Divisions
        {
            get { return divisions; }
            private set
            {
                divisions = value;
                OnPropertyChanged();
            }
        }
        private DivisionVM selectedDivision;
        public DivisionVM SelectedDivision
        {
            get { return selectedDivision; }
            set
            {
                selectedDivision = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Ctors
        public WorkStationDialogViewModel(WorkStationVM workStation, string connectionString) : base(workStation)
        {
            factoryFloorService = new FactoryFloorService(connectionString);
            FactoryFloors = factoryFloorService.GetAll();
            divisionService = new DivisionService(connectionString);
            if (CurrentItem.DivisionId == 0) return;
            SelectedFactoryFloor = CurrentItem.Division.FactoryFloor;
            LoadDivisions();
            SelectedDivision = CurrentItem.Division;
        }
        #endregion

        #region Commands
        private ICommand loadDivisionsCommand = null;

        public ICommand LoadDivisionsCommand => loadDivisionsCommand ?? (loadDivisionsCommand = new RelayCommand(LoadDivisions));

        private void LoadDivisions(object parameter)
        {
            CurrentItem.Division = null;
            CurrentItem.DivisionId = 0;
            LoadDivisions();
        }

        private void LoadDivisions()
        {
            if (SelectedFactoryFloor == null) return;
            try
            {
                Divisions = divisionService.GetWhere(SelectedFactoryFloor.FactoryFloorId);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка получения данных участка");
            }
        }

        private ICommand initializeFactoryFloorCommand = null;
        public ICommand InitializeFactoryFloorCommand => initializeFactoryFloorCommand ?? (initializeFactoryFloorCommand = new RelayCommand(InitializeFactoryFloor));
        private void InitializeFactoryFloor(object parameter)
        {
            if (CurrentItem.DivisionId == 0) return;
            CurrentItem.Division.FactoryFloor = SelectedFactoryFloor;
        }
        #endregion
    }
}
