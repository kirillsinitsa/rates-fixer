using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Models;
using System.Collections.ObjectModel;
using RatesFixer.BLL.Services;
using RatesFixer.Infrastructure;
using RatesFixer.ViewModels.BaseViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace RatesFixer.ViewModels.Employee
{
    public class EmployeeDialogViewModel : DialogViewModel<EmployeeVM>
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

        private readonly IWorkStationService workStationService;
        private ObservableCollection<WorkStationVM> workStations;
        public ObservableCollection<WorkStationVM> WorkStations
        {
            get { return workStations; }
            private set
            {
                workStations = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Ctors
        public EmployeeDialogViewModel(EmployeeVM employee, string connectionString) : base(employee)
        {
            factoryFloorService = new FactoryFloorService(connectionString);
            FactoryFloors = factoryFloorService.GetAll();
            divisionService = new DivisionService(connectionString);
            workStationService = new WorkStationService(connectionString);
            try
            {
                if (CurrentItem.WorkStationId != 0)
                {
                    SelectedFactoryFloor = CurrentItem.WorkStation.Division.FactoryFloor;
                    LoadDivisions(null);
                    SelectedDivision = CurrentItem.WorkStation.Division;
                    LoadWorkStations(null);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка получения данных работника");
            }
        }
        #endregion

        #region Commands
        private ICommand loadDivisionsCommand = null;
        public ICommand LoadDivisionsCommand => loadDivisionsCommand ?? (loadDivisionsCommand = new RelayCommand(LoadDivisions));
        private void LoadDivisions(object parameter)
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

        private ICommand loadWorkStationsCommand = null;
        public ICommand LoadWorkStationsCommand => loadWorkStationsCommand ?? (loadWorkStationsCommand = new RelayCommand(LoadWorkStations));
        private void LoadWorkStations(object parameter)
        {
            if (SelectedDivision == null) return;
            try
            {
                WorkStations = workStationService.GetWhere(SelectedDivision.DivisionId);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка получения данных рабочего места");
            }
        }

        private ICommand initializeDivisionAndFactoryFloorCommand = null;
        public ICommand InitializeDivisionAndFactoryFloorCommand => initializeDivisionAndFactoryFloorCommand ?? (initializeDivisionAndFactoryFloorCommand = new RelayCommand(InitializeDivisionAndFactoryFloor));
        private void InitializeDivisionAndFactoryFloor(object parameter)
        {
            if (CurrentItem.WorkStation == null) return;
            CurrentItem.WorkStation.Division = SelectedDivision;
            CurrentItem.WorkStation.Division.FactoryFloor = SelectedFactoryFloor;
        }
        #endregion
    }
}
