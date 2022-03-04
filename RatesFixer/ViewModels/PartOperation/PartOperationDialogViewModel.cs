using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Models;
using System.Collections.ObjectModel;
using RatesFixer.BLL.Services;
using RatesFixer.Infrastructure;
using RatesFixer.ViewModels.BaseViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RatesFixer.ViewModels.PartOperation
{
    public class PartOperationDialogViewModel : DialogViewModel<PartOperationVM>
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

        private IDivisionService divisionService;
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

        private IOperationService operationService;
        private ObservableCollection<OperationVM> operations;
        public ObservableCollection<OperationVM> Operations
        {
            get { return operations; }
            private set
            {
                operations = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Ctors
        public PartOperationDialogViewModel(PartOperationVM partOperation, string connectionString) : base(partOperation)
        {
            factoryFloorService = new FactoryFloorService(connectionString);
            FactoryFloors = factoryFloorService.GetAll();
            divisionService = new DivisionService(connectionString);
            operationService = new OperationService(connectionString);
            Operations = operationService.GetAll();
            if (CurrentItem.PartOperationId == 0) CurrentItem.Percentage = 100;
            try
            {
                if (CurrentItem.DivisionId != 0)
                {
                    CurrentItem.Division = divisionService.Get(CurrentItem.DivisionId);
                    SelectedFactoryFloor = factoryFloorService.Get(CurrentItem.Division.FactoryFloorId);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка получения данных операции по тех. процессу");
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

        private ICommand initializeFactoryFloorCommand = null;

        public ICommand InitializeFactoryFloorCommand => initializeFactoryFloorCommand ?? (initializeFactoryFloorCommand = new RelayCommand(InitializeFactoryFloor));

        private void InitializeFactoryFloor(object parameter)
        {
            if (CurrentItem.Division == null) return;
            CurrentItem.Division.FactoryFloor = SelectedFactoryFloor;
        }
        #endregion
    }
}
