using Prism.Events;
using RatesFixer.BLL.BusinessLogic;
using RatesFixer.BLL.Models;
using RatesFixer.Infrastructure;
using RatesFixer.ViewModels.BaseViewModels;
using RatesFixer.Views.StandingOrder;
using System.Collections.Generic;
using System.Windows.Input;

namespace RatesFixer.ViewModels.StandingOrder
{
    public class StandingOrderViewModel : BaseViewModel
    {
        #region Fields and Properties
        private string connectionString;
        private IEventAggregator eventAggregator;

        private OrderVM currentOrder;
        public OrderVM CurrentOrder
        {
            get => currentOrder;
            set
            {
                currentOrder = value;
                OnPropertyChanged();
            }
        }

        public bool ToPrint { get; set; } = false;
        #endregion

        #region Ctors
        public StandingOrderViewModel(OrderVM order, IEventAggregator eventAggregator, string connectionString)
        {
            CurrentOrder = order;
            this.connectionString = connectionString;
            this.eventAggregator = eventAggregator;
            SubscribeToEvents();
        }
        #endregion

        #region Methods
        private void SubscribeToEvents()
        {
            eventAggregator.GetEvent<FactoryFloorDeletedEvent>().Subscribe(DeleteFactoryFloor);
            eventAggregator.GetEvent<FactoryFloorChangedEvent>().Subscribe(ChangeFactoryFloor);
            eventAggregator.GetEvent<DivisionDeletedEvent>().Subscribe(DeleteDivision);
            eventAggregator.GetEvent<DivisionChangedEvent>().Subscribe(ChangeDivision);
            eventAggregator.GetEvent<PartDeletedEvent>().Subscribe(DeletePart);
            eventAggregator.GetEvent<PartChangedEvent>().Subscribe(ChangePart);
            eventAggregator.GetEvent<PartOperationDeletedEvent>().Subscribe(DeletePartOperation);
            eventAggregator.GetEvent<PartOperationChangedEvent>().Subscribe(ChangePartOperation);
            eventAggregator.GetEvent<EmployeeDeletedEvent>().Subscribe(DeleteEmployee);
            eventAggregator.GetEvent<EmployeeChangedEvent>().Subscribe(ChangeEmployee);
            eventAggregator.GetEvent<WageRateDeletedEvent>().Subscribe(DeleteWageRate);
            eventAggregator.GetEvent<WageRateChangedEvent>().Subscribe(ChangeWageRate);
        }
        private void DeleteFactoryFloor(int factoryFloorId)
        {
            if (CurrentOrder.FactoryFloorId == factoryFloorId)
            {
                CurrentOrder.FactoryFloorId = 0;
                CurrentOrder.FactoryFloor = null;
            }
        }
        private void ChangeFactoryFloor(FactoryFloorVM factoryFloor)
        {
            if (CurrentOrder.FactoryFloorId == factoryFloor.FactoryFloorId)
            {
                CurrentOrder.FactoryFloor = factoryFloor;
            }
        }
        private void DeleteDivision(int divisionId)
        {
            if (CurrentOrder.DivisionId == divisionId)
            {
                CurrentOrder.DivisionId = 0;
                CurrentOrder.Division = null;
            }
        }
        private void ChangeDivision(DivisionVM division)
        {
            if (CurrentOrder.DivisionId == division.DivisionId)
            {
                CurrentOrder.Division = division;
            }
        }
        private void DeletePart(int partId)
        {
            if (CurrentOrder.PartId == partId)
            {
                CurrentOrder.PartId = 0;
                CurrentOrder.Part = null;
                CurrentOrder.OrderEntries.Clear();
            }
        }
        private void ChangePart(PartVM part)
        {
            if (CurrentOrder.PartId == part.PartId)
            {
                CurrentOrder.Part = part;
            }
        }
        private void DeletePartOperation(int partOperationId)
        {
            for (var i =0; i < CurrentOrder.OrderEntries.Count; i++)
            {
                if (CurrentOrder.OrderEntries[i].PartOperationId == partOperationId) CurrentOrder.OrderEntries.RemoveAt(i);
            }
        }
        private void ChangePartOperation(PartOperationVM partOperation)
        {
            var paymentCalculator = new PaymentCalculator();

            foreach (OrderEntryVM orderEntry in CurrentOrder.OrderEntries)
            {
                if (orderEntry.PartOperationId == partOperation.PartOperationId)
                {
                    var rateTimeChanged = (orderEntry.PartOperation.RateTime == partOperation.RateTime);
                    orderEntry.PartOperation = partOperation;
                    if (rateTimeChanged) paymentCalculator.CalculateStandardHours(orderEntry);
                }
            }
        }
        private void DeleteEmployee(int employeeId)
        {
            foreach (OrderEntryVM orderEntry in CurrentOrder.OrderEntries)
            {
                if (orderEntry.EmployeeId == employeeId)
                {
                    orderEntry.EmployeeId = 0;
                    orderEntry.Employee = null;
                }
            }
        }
        private void ChangeEmployee(EmployeeVM employee)
        {
            foreach (OrderEntryVM orderEntry in CurrentOrder.OrderEntries)
            {
                if (orderEntry.EmployeeId == employee.EmployeeId)
                {
                    orderEntry.Employee = employee;
                }
            }
        }
        private void DeleteWageRate(int wageRateId)
        {
            foreach (OrderEntryVM orderEntry in CurrentOrder.OrderEntries)
            {
                if (orderEntry.PartOperation.WageRateId == wageRateId)
                {
                    orderEntry.PartOperation.WageRateId = 0;
                    orderEntry.PartOperation.WageRateValue = 0;
                    orderEntry.UsefulProductsPayment.Sum = 0;
                    orderEntry.DefectProductsPayment.Sum = 0;
                }
            }
        }
        private void ChangeWageRate(WageRateVM wageRate)
        {
            var paymentCalculator = new PaymentCalculator();

            foreach (OrderEntryVM orderEntry in CurrentOrder.OrderEntries)
            {
                if (orderEntry.PartOperation.WageRateId == wageRate.WageRateId)
                {
                    orderEntry.PartOperation.WageRateValue = wageRate.WageRateValue;
                    paymentCalculator.CalculateSums(orderEntry);
                }
            }
        }
        #endregion

        #region Commands
        private ICommand deleteOrderCommand = null;

        public ICommand DeleteOrderCommand => deleteOrderCommand ?? (deleteOrderCommand = new RelayCommand(DeleteOrder));

        private void DeleteOrder(object obj)
        {
            new StandingOrderHandler(eventAggregator, connectionString).DeleteOrder(CurrentOrder);
        }

        private ICommand editOrderCommand = null;

        public ICommand EditOrderCommand => editOrderCommand ?? (editOrderCommand = new RelayCommand(EditOrder));

        private void EditOrder(object obj)
        {
            var viewModel = new StandingOrderEditorViewModel(new OrderVM(CurrentOrder), eventAggregator, connectionString);
            var view = new StandingOrderEditorWindow(viewModel);
            view.Show();
        }
        #endregion
    }
}
