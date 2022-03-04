using Prism.Events;
using RatesFixer.BLL.BusinessLogic;
using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Models;
using System.Collections.ObjectModel;
using RatesFixer.BLL.Services;
using RatesFixer.Infrastructure;
using RatesFixer.ViewModels.BaseViewModels;
using RatesFixer.Views.StandingOrder;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Data.Entity.Validation;
using RatesFixer.BLL.Infrastructure;
using RatesFixer.ErrorsHelper;

namespace RatesFixer.ViewModels.StandingOrder
{
    public class StandingOrderEditorViewModel : DialogViewModel<OrderVM>
    {
        #region Fields and Properties
        private readonly string connectionString;
        private readonly IEventAggregator eventAggregator;               
        private readonly IWageRateService wageRateService;
        private readonly IOperationService operationService;
        private readonly IFactoryFloorService factoryFloorService;        
        private readonly IDivisionService divisionService;        
        private readonly IWorkStationService workStationService;
        private readonly IEmployeeService employeeService;       
        private readonly IPartService partService;        
        private readonly IPartOperationService partOperationService;
        private OrderVM originalOrder;

        private ObservableCollection<WorkStationVM> workStations;

        private OrderEntryVM currentOrderEntry = null;
        public OrderEntryVM CurrentOrderEntry
        {
            get => currentOrderEntry ?? (currentOrderEntry = CurrentItem.OrderEntries.Count != 0 ? new OrderEntryVM(CurrentItem.OrderEntries.First()) : new OrderEntryVM());
            set
            {
                currentOrderEntry = value;
                OnPropertyChanged();
            }
        }

        private OrderEntryVM selectedOrderEntry = null;
        public OrderEntryVM SelectedOrderEntry
        {
            get => selectedOrderEntry ?? (selectedOrderEntry = CurrentItem.OrderEntries.Count != 0 ? new OrderEntryVM(CurrentItem.OrderEntries.First()) : new OrderEntryVM());
            set
            {
                selectedOrderEntry = value;
                OnPropertyChanged();
            }
        }

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

        private ObservableCollection<PartVM> parts;
        public ObservableCollection<PartVM> Parts
        {
            get { return parts; }
            private set
            {
                parts = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PartOperationVM> partOperations;
        public ObservableCollection<PartOperationVM> PartOperations
        {
            get { return partOperations; }
            private set
            {
                partOperations = value;
                OnPropertyChanged();
            }
        }

        public Action CloseAction { get; set; }
        #endregion

        #region Ctors
        public StandingOrderEditorViewModel(OrderVM order, IEventAggregator eventAggregator, string connectionString) : base(order)
        {
            this.connectionString = connectionString;
            this.eventAggregator = eventAggregator;
            if (order != null) originalOrder = new OrderVM(order);
            SubscribeToEvents();
            factoryFloorService = new FactoryFloorService(connectionString);
            divisionService = new DivisionService(connectionString);
            workStationService = new WorkStationService(connectionString);
            employeeService = new EmployeeService(connectionString);
            partService = new PartService(connectionString);
            partOperationService = new PartOperationService(connectionString);
            operationService = new OperationService(connectionString);
            wageRateService = new WageRateService(connectionString);
            RefreshFactoryFloors();
            InitializeFields();
        }
        #endregion

        #region Methods

        private void SubscribeToEvents()
        {
            eventAggregator.GetEvent<ApplicationClosedEvent>().Subscribe(Close);
            eventAggregator.GetEvent<FactoryFloorCreatedEvent>().Subscribe(AddFactoryFloor);
            eventAggregator.GetEvent<FactoryFloorDeletedEvent>().Subscribe(DeleteFactoryFloor);
            eventAggregator.GetEvent<FactoryFloorChangedEvent>().Subscribe(ChangeFactoryFloor);
            eventAggregator.GetEvent<DivisionCreatedEvent>().Subscribe(AddDivision);
            eventAggregator.GetEvent<DivisionDeletedEvent>().Subscribe(DeleteDivision);
            eventAggregator.GetEvent<DivisionChangedEvent>().Subscribe(ChangeDivision);
            eventAggregator.GetEvent<PartCreatedEvent>().Subscribe(AddPart);
            eventAggregator.GetEvent<PartDeletedEvent>().Subscribe(DeletePart);
            eventAggregator.GetEvent<PartChangedEvent>().Subscribe(ChangePart);
            eventAggregator.GetEvent<PartOperationCreatedEvent>().Subscribe(AddPartOperation);
            eventAggregator.GetEvent<PartOperationDeletedEvent>().Subscribe(DeletePartOperation);
            eventAggregator.GetEvent<PartOperationChangedEvent>().Subscribe(ChangePartOperation);
            eventAggregator.GetEvent<EmployeeCreatedEvent>().Subscribe(AddEmployee);
            eventAggregator.GetEvent<EmployeeDeletedEvent>().Subscribe(DeleteEmployee);
            eventAggregator.GetEvent<EmployeeChangedEvent>().Subscribe(ChangeEmployee);
            eventAggregator.GetEvent<WageRateCreatedEvent>().Subscribe(ChangeWageRate);
            eventAggregator.GetEvent<WageRateDeletedEvent>().Subscribe(DeleteWageRate);
            eventAggregator.GetEvent<WageRateChangedEvent>().Subscribe(ChangeWageRate);
        }
        private void RefreshFactoryFloors()
        {
            FactoryFloors = factoryFloorService.GetAll();
        }
        private void AddFactoryFloor(FactoryFloorVM factoryFloor)
        {
            FactoryFloors.Add(factoryFloor);
        }
        private void DeleteFactoryFloor(int factoryFloorId)
        {
            if (CurrentItem.FactoryFloorId == factoryFloorId)
            {
                CurrentItem.FactoryFloorId = 0;
                CurrentItem.FactoryFloor = null;
                CurrentItem.DivisionId = 0;
                CurrentItem.Division = null;
                CurrentItem.PartId = 0;
                CurrentItem.Part = null;
                CurrentItem.OrderEntries.Clear();
                CurrentOrderEntry = new OrderEntryVM();
            }
            foreach (FactoryFloorVM factoryFloor in FactoryFloors)
            {
                if (factoryFloor.FactoryFloorId == factoryFloorId)
                {
                    FactoryFloors.Remove(factoryFloor);
                    return;
                }
            }
        }
        private void ChangeFactoryFloor(FactoryFloorVM factoryFloor)
        {
            for (var i = 0; i < FactoryFloors.Count; i++)
            {
                if (FactoryFloors[i].FactoryFloorId == factoryFloor.FactoryFloorId)
                {
                    FactoryFloors[i] = factoryFloor;
                    return;
                }
            }
            if (CurrentItem.FactoryFloorId == factoryFloor.FactoryFloorId)
            {
                CurrentItem.FactoryFloorId = factoryFloor.FactoryFloorId;
                CurrentItem.FactoryFloor = factoryFloor;
            }
        }
        private void AddDivision(DivisionVM division)
        {
            if (CurrentItem.FactoryFloorId == division.FactoryFloorId)
            {
                Divisions.Add(division);
            } 
        }
        private void DeleteDivision(int divisionId)
        {
            for (int i = 0; i < Divisions.Count; i++)
            {
                if (Divisions[i].DivisionId == divisionId)
                {
                    Divisions.RemoveAt(i);
                    break;
                }
            }
            if (CurrentItem.DivisionId == divisionId)
            {
                CurrentItem.DivisionId = 0;
                CurrentItem.Division = null;
                CurrentItem.PartId = 0;
                CurrentItem.Part = null;
                CurrentItem.OrderEntries.Clear();
                CurrentOrderEntry = new OrderEntryVM();
            }
        }
        private void ChangeDivision(DivisionVM division)
        {
            for (int i = 0; i < Divisions.Count; i++)
            {
                if (Divisions[i].DivisionId == division.DivisionId)
                {
                    Divisions[i] = division;
                    break;
                }
            }
            if (CurrentItem.DivisionId == division.DivisionId)
            {
                CurrentItem.Division = division;
            }
        }
        private void AddPart(PartVM part)
        {
            foreach (PartOperationVM partOperation in part.Operations)
            {
                if (partOperation.DivisionId == CurrentItem.DivisionId)
                {
                    Parts.Add(part);
                    return;
                }
            }
        }
        private void DeletePart(int partId)
        {
            for (var i = 0; i < Parts.Count; i++)
            {
                if (Parts[i].PartId == partId)
                {
                    Parts.RemoveAt(i);
                    break;
                }
            }
            if (CurrentItem.PartId == partId)
            {
                CurrentItem.PartId = 0;
                CurrentItem.Part = null;
                CurrentItem.OrderEntries.Clear();
                CurrentOrderEntry = new OrderEntryVM();
            }
        }
        private void ChangePart(PartVM part)
        {
            for (var i = 0; i < Parts.Count; i++)
            {
                if (Parts[i].PartId == part.PartId)
                {
                    Parts[i] = part;
                    break;
                }
            }
            if (CurrentItem.PartId == part.PartId)
            {
                CurrentItem.PartId = part.PartId;
                CurrentItem.Part = part;
            }
        }
        private void AddPartOperation(PartOperationVM partOperation)
        {
            if (partOperation.PartId == CurrentItem.PartId)
            {
                PartOperations.Add(partOperation);
            }
        }
        private void DeletePartOperation(int partOperationId)
        {
            var partOperationFound = false;

            for (var i = 0; i < PartOperations.Count; i++)
            {
                if (PartOperations[i].PartOperationId == partOperationId)
                {
                    PartOperations.RemoveAt(i);
                    partOperationFound = true;
                    break;
                }
            }
            if (!partOperationFound) return;
            if (CurrentOrderEntry.PartOperationId == partOperationId) CurrentOrderEntry = new OrderEntryVM();
            foreach (OrderEntryVM orderEntry in CurrentItem.OrderEntries)
            {                
                if (orderEntry.PartOperationId == partOperationId) CurrentItem.OrderEntries.Remove(orderEntry);
            }
        }
        private void ChangePartOperation(PartOperationVM partOperation)
        {
            var partOperationFound = false;

            for (var i = 0; i < PartOperations.Count; i++)
            {
                if (PartOperations[i].PartOperationId == partOperation.PartOperationId)
                {
                    PartOperations[i] = partOperation;
                    partOperationFound = true;
                    break;
                }
            }
            if (!partOperationFound) return;

            var paymentCalculator = new PaymentCalculator();

            foreach (OrderEntryVM orderEntry in CurrentItem.OrderEntries)
            {
                if (orderEntry.PartOperationId == partOperation.PartOperationId)
                {
                    var rateTimeChanged = (orderEntry.PartOperation.RateTime == partOperation.RateTime);
                    orderEntry.PartOperation = partOperation;
                    if (rateTimeChanged) paymentCalculator.CalculateStandardHours(orderEntry);
                }
            }
        }
        private void AddEmployee(EmployeeVM employee)
        {
            for (var i = 0; i < workStations.Count; i++)
            {
                if (workStations[i].WorkStationId == employee.WorkStationId)
                {
                    Employees.Add(employee);
                    return;
                }
            }
        }
        private void DeleteEmployee(int employeeId)
        {
            var employeeFound = false;

            for (var i = 0; i < Employees.Count; i++)
            {
                if (Employees[i].EmployeeId == employeeId)
                {
                    Employees.RemoveAt(i);
                    employeeFound = true;
                    break;
                }
            }
            if (!employeeFound) return;
            foreach (OrderEntryVM orderEntry in CurrentItem.OrderEntries)
            {
                if (orderEntry.EmployeeId == employeeId)
                {
                    orderEntry.EmployeeId = 0;
                    orderEntry.Employee = null;
                }
            }
            if (CurrentOrderEntry.EmployeeId == employeeId)
            {
                CurrentOrderEntry.EmployeeId = 0;
                CurrentOrderEntry.Employee = null;
            }
        }
        private void ChangeEmployee(EmployeeVM employee)
        {
            var employeeFound = false;

            for (var i = 0; i < Employees.Count; i++)
            {
                if (Employees[i].EmployeeId == employee.EmployeeId)
                {
                    Employees[i] = employee;
                    employeeFound = true;
                    break;
                }
            }
            if (!employeeFound) return;
            foreach (OrderEntryVM orderEntry in CurrentItem.OrderEntries)
            {
                if (orderEntry.EmployeeId == employee.EmployeeId)
                {
                    orderEntry.Employee = employee;
                }
            }
            if (CurrentOrderEntry.EmployeeId == employee.EmployeeId)
            {
                CurrentOrderEntry.Employee = employee;
            }
        }
        
        private void DeleteWageRate(int wageRateId)
        {
            DeleteOrderEntryWageRate(CurrentOrderEntry, wageRateId);
            foreach (OrderEntryVM orderEntry in CurrentItem.OrderEntries)
            {
                DeleteOrderEntryWageRate(orderEntry, wageRateId);
            }
        }

        private void DeleteOrderEntryWageRate(OrderEntryVM orderEntry, int wageRateId)
        {
            if (orderEntry.PartOperation.WageRateId == wageRateId)
            {
                orderEntry.PartOperation.WageRateId = 0;
                orderEntry.PartOperation.WageRateValue = 0;
                orderEntry.UsefulProductsPayment.Sum = 0;
                orderEntry.DefectProductsPayment.Sum = 0;
            }
        }
        private void ChangeWageRate(WageRateVM wageRate)
        {
            var paymentCalculator = new PaymentCalculator();
            ChangeOrderEntryWageRate(CurrentOrderEntry, wageRate, paymentCalculator);
            foreach (OrderEntryVM orderEntry in CurrentItem.OrderEntries)
            {
                ChangeOrderEntryWageRate(orderEntry, wageRate, paymentCalculator);
            }
        }
        private void ChangeOrderEntryWageRate(OrderEntryVM orderEntry, WageRateVM wageRate, PaymentCalculator paymentCalculator)
        {
            if (orderEntry.PartOperationId == wageRate.PartOperationId)
            {
                orderEntry.PartOperation.WageRateId = wageRate.WageRateId;
                orderEntry.PartOperation.WageRateValue = wageRate.WageRateValue;
                paymentCalculator.CalculateSums(orderEntry);
            }
        }
        private void InitializeFields()
        {
            if (CurrentItem.OrderId == 0)
            {
                CurrentItem.Date = DateTime.Now;
            }
            else
            {
                LoadDivisions(null);
                LoadParts();
                LoadPartOperations(null);
                LoadEmployees();
            }
        }
        #endregion

        #region Commands
        private ICommand closeCommand = null;
        public ICommand CloseCommand => closeCommand ?? (closeCommand = new RelayCommand(Close));
        public void Close(object parameter)
        {
            Close();
        }
        public void Close()
        {
            CloseAction();
        }
        private ICommand loadDivisionsCommand = null;
        public ICommand LoadDivisionsCommand => loadDivisionsCommand ?? (loadDivisionsCommand = new RelayCommand(LoadDivisions));
        private void LoadDivisions(object parameter)
        {
            if (CurrentItem.FactoryFloorId == 0) return;
            try
            {
                Divisions = divisionService.GetWhere(CurrentItem.FactoryFloorId);
            }
            catch (DbEntityValidationException e)
            {
                MessageBox.Show(e.EntityValidationErrorsToString());
            }
            catch (ValidationException e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка добавления рапорта о выработке");
            }
        }

        private ICommand loadPartsAndEmployeesCommand = null;
        public ICommand LoadPartsAndEmployeesCommand => loadPartsAndEmployeesCommand ?? (loadPartsAndEmployeesCommand = new RelayCommand(LoadPartsAndEmployees));
        private void LoadPartsAndEmployees(object parameter)
        {
            LoadParts();
            LoadEmployees();
        }

        private void LoadParts()
        {
            if (CurrentItem.DivisionId == 0) return;
            try
            {
                Parts = partService.Find(partOperationService.FindParts(CurrentItem.DivisionId));
            }
            catch (DbEntityValidationException e)
            {
                MessageBox.Show(e.EntityValidationErrorsToString());
            }
            catch (ValidationException e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка добавления рапорта о выработке");
            }
        }

        private void LoadEmployees()
        {
            if (CurrentItem.DivisionId == 0) return;
            try
            {
                workStations = workStationService.GetWhere(CurrentItem.DivisionId);

                Employees = employeeService.FindWhere(workStations.Select(o => o.WorkStationId));
            }
            catch (DbEntityValidationException e)
            {
                MessageBox.Show(e.EntityValidationErrorsToString());
            }
            catch (ValidationException e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка добавления рапорта о выработке");
            }
        }

        private ICommand loadPartOperationsCommand = null;
        public ICommand LoadPartOperationsCommand => loadPartOperationsCommand ?? (loadPartOperationsCommand = new RelayCommand(LoadPartOperations));
        private void LoadPartOperations(object parameter)
        {
            try
            {
                PartOperations = partOperationService.GetWhere(CurrentItem.PartId);
            }
            catch (DbEntityValidationException e)
            {
                MessageBox.Show(e.EntityValidationErrorsToString());
            }
            catch (ValidationException e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка добавления рапорта о выработке");
            }
        }

        private ICommand selectPartOperationCommand = null;
        public ICommand SelectPartOperationCommand => selectPartOperationCommand ?? (selectPartOperationCommand = new RelayCommand(SelectPartOperation));
        private void SelectPartOperation(object parameter)
        {
            if (CurrentOrderEntry.PartOperationId == 0) return;
            try
            {
                CurrentOrderEntry.PartOperation = partOperationService.Get(CurrentOrderEntry.PartOperationId);
                CurrentOrderEntry.PartOperation.Operation = operationService.Get(CurrentOrderEntry.PartOperation.OperationId);
                CurrentOrderEntry.PartOperation.WageRateValue = wageRateService.Get(CurrentOrderEntry.PartOperation.WageRateId).WageRateValue;
                OnPropertyChanged("CurrentOrderEntry");
            }
            catch (DbEntityValidationException e)
            {
                MessageBox.Show(e.EntityValidationErrorsToString());
            }
            catch (ValidationException e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка добавления рапорта о выработке");
            }
        }

        private ICommand selectEmployeeCommand = null;
        public ICommand SelectEmployeeCommand => selectEmployeeCommand ?? (selectEmployeeCommand = new RelayCommand(SelectEmployee));
        private void SelectEmployee(object parameter)
        {
            if (CurrentOrderEntry.EmployeeId == 0) return;
            try
            {
                CurrentOrderEntry.Employee = employeeService.Get(CurrentOrderEntry.EmployeeId);
                OnPropertyChanged("CurrentOrderEntry");
            }
            catch (DbEntityValidationException e)
            {
                MessageBox.Show(e.EntityValidationErrorsToString());
            }
            catch (ValidationException e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка добавления рапорта о выработке");
            }
        }

        private ICommand calculatePaymentCommand = null;
        public ICommand CalculatePaymentCommand => calculatePaymentCommand ?? (calculatePaymentCommand = new RelayCommand(CalculatePayment));
        private void CalculatePayment(object parameter)
        {
            try
            {
                new PaymentCalculator().CalculatePayment(CurrentOrderEntry);               
            }
            catch (DbEntityValidationException e)
            {
                MessageBox.Show(e.EntityValidationErrorsToString());
            }
            catch (ValidationException e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка добавления рапорта о выработке");
            }
        }

        private ICommand selectOrderEntryCommand = null;
        public ICommand SelectOrderEntryCommand => selectOrderEntryCommand ?? (selectOrderEntryCommand = new RelayCommand(SelectOrderEntry));
        private void SelectOrderEntry(object parameter)
        {
            CurrentOrderEntry = new OrderEntryVM(SelectedOrderEntry);
        }

        private ICommand addOrderEntryCommand = null;
        public ICommand AddOrderEntryCommand => addOrderEntryCommand ?? (addOrderEntryCommand = new RelayCommand(AddOrderEntry));
        private void AddOrderEntry(object parameter)
        {
            try
            {
                if (!CurrentItem.OrderEntries.Contains(CurrentOrderEntry))
                {
                    CurrentOrderEntry.OrderEntryId = 0;
                    CurrentItem.OrderEntries.Add(CurrentOrderEntry);
                    SelectedOrderEntry = CurrentItem.OrderEntries.Last();
                }
            }
            catch (DbEntityValidationException e)
            {
                MessageBox.Show(e.EntityValidationErrorsToString());
            }
            catch (ValidationException e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка добавления рапорта о выработке");
            }
        }

        private ICommand deleteOrderEntryCommand = null;
        public ICommand DeleteOrderEntryCommand => deleteOrderEntryCommand ?? (deleteOrderEntryCommand = new RelayCommand(DeleteOrderEntry));
        private void DeleteOrderEntry(object parameter)
        {
            try
            {
                if (CurrentItem.OrderEntries.Contains(SelectedOrderEntry))
                {
                    CurrentItem.OrderEntries.Remove(SelectedOrderEntry);
                    SelectedOrderEntry = CurrentItem.OrderEntries.FirstOrDefault();
                }
            }
            catch (DbEntityValidationException e)
            {
                MessageBox.Show(e.EntityValidationErrorsToString());
            }
            catch (ValidationException e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка добавления рапорта о выработке");
            }
        }

        private ICommand editOrderEntryCommand = null;
        public ICommand EditOrderEntryCommand => editOrderEntryCommand ?? (editOrderEntryCommand = new RelayCommand(EditOrderEntry));
        private void EditOrderEntry(object parameter)
        {
            try
            {
                SelectedOrderEntry.Copy(CurrentOrderEntry);
                OnPropertyChanged("SelectedOrderEntry");
                OnPropertyChanged("CurrentItem.OrderEntries");
            }
            catch (DbEntityValidationException e)
            {
                MessageBox.Show(e.EntityValidationErrorsToString());
            }
            catch (ValidationException e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка добавления рапорта о выработке");
            }
        }

        private ICommand saveOrderCommand = null;
        public ICommand SaveOrderCommand => saveOrderCommand ?? (saveOrderCommand = new RelayCommand(SaveOrder));
        private void SaveOrder(object parameter)
        {
            var standingOrderHandler = new StandingOrderHandler(eventAggregator, connectionString);

            if (CurrentItem.OrderId == 0)
            {
                standingOrderHandler.AddOrder(CurrentItem);
            }
            else
            {
                standingOrderHandler.EditOrder(CurrentItem, originalOrder);
            }
        }
        #endregion
    }
}
