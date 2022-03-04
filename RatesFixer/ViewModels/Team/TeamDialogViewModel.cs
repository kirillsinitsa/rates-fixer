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

namespace RatesFixer.ViewModels.Team
{
    public class TeamDialogViewModel : DialogViewModel<TeamVM>
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

        private readonly IWorkStationService workStationService;

        private readonly IEmployeeService employeeService;
        private ObservableCollection<EmployeeVM> employees;
        public ObservableCollection<EmployeeVM> Employees
        {
            get { return employees; }
            private set
            {
                employees = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Ctors
        public TeamDialogViewModel(TeamVM team, string connectionString) : base(team)
        {
            factoryFloorService = new FactoryFloorService(connectionString);
            divisionService = new DivisionService(connectionString);
            workStationService = new WorkStationService(connectionString);
            employeeService = new EmployeeService(connectionString);
            InitializeFields();
        }
        #endregion

        #region Methods
        private void InitializeFields()
        {
            FactoryFloors = factoryFloorService.GetAll();
            try
            {
                if (CurrentItem.DivisionId != 0)
                {
                    SelectedFactoryFloor = CurrentItem.Division.FactoryFloor;
                    LoadDivisions(null);
                    LoadEmployees(null);
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
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка получения данных бригады");
            }
        }

        private ICommand loadEmployeesCommand = null;
        public ICommand LoadEmployeesCommand => loadEmployeesCommand ?? (loadEmployeesCommand = new RelayCommand(LoadEmployees));
        private void LoadEmployees(object parameter)
        {
            if (CurrentItem.DivisionId == 0) return;
            try
            {
                Employees = employeeService.FindWhere(workStationService.GetWhere(CurrentItem.DivisionId).Select(o => o.WorkStationId));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка получения данных бригады");
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
