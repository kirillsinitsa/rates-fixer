using Prism.Events;
using RatesFixer.BLL.Models;
using System.Collections.ObjectModel;
using RatesFixer.BLL.Services;
using RatesFixer.ViewModels.BaseViewModels;
using RatesFixer.Views.StandingOrder;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace RatesFixer.ViewModels.StandingOrder
{
    public class StandingOrderViewerViewModel : BaseViewModel
    {
        #region Properties
        private readonly string connectionString;
        private readonly IEventAggregator eventAggregator;
        private Dictionary<int, PrintingStandingOrderUC> standingOrderControls;

        private WrapPanel ordersList;
        public WrapPanel OrdersList
        {
            get => ordersList;
            set
            {
                ordersList = value;
                OnPropertyChanged();
            }
        }

        private bool allToPrint = false;
        public bool AllToPrint
        {
            get => allToPrint;
            set
            {
                allToPrint = value;
                CheckAllOrdersToPrint();
                OnPropertyChanged();
            }
        }
        #endregion

        #region Ctors
        public StandingOrderViewerViewModel(ICollection<OrderVM> orders, IEventAggregator eventAggregator, string connectionString)
        {
            this.eventAggregator = eventAggregator;
            this.connectionString = connectionString;            
            this.eventAggregator.GetEvent<OrderCreatedEvent>().Subscribe(AddOrderToOrdersList);
            this.eventAggregator.GetEvent<OrderChangedEvent>().Subscribe(ChangeOrderInOrdersList);
            this.eventAggregator.GetEvent<OrderDeletedEvent>().Subscribe(DeleteOrderFromOrdersList);
            InitializeOrdersList(orders);
        }
        #endregion

        #region Methods
        private void AddOrderToOrdersList(OrderVM order)
        {
            standingOrderControls.Add(order.OrderId,
                new PrintingStandingOrderUC(new PrintingStandingOrderViewModel(new StandingOrderUC(
                    new StandingOrderViewModel(order, eventAggregator, connectionString)))));
            OrdersList.Children.Add(standingOrderControls[order.OrderId]);
        }
        private void ChangeOrderInOrdersList(OrderVM order)
        {
            OrdersList.Children.Remove(standingOrderControls[order.OrderId]);
            standingOrderControls.Remove(order.OrderId);
            AddOrderToOrdersList(order);
        }

        private void DeleteOrderFromOrdersList(int orderId)
        {            
            OrdersList.Children.Remove(standingOrderControls[orderId]);
            standingOrderControls.Remove(orderId);
            if (OrdersList.Children.Count == 0) eventAggregator.GetEvent<OrdersClosedEvent>().Publish();
        }

        private void InitializeOrdersList(ICollection<OrderVM> orders)
        {
            standingOrderControls = new Dictionary<int, PrintingStandingOrderUC>();
            InitializeStandingOrderControls(orders);
            OrdersList = InitializedWrapPanel();
        }

        private WrapPanel InitializedWrapPanel()
        {
            var wrapPanel = new WrapPanel() { Orientation = Orientation.Vertical, Width = 995 };
            foreach (PrintingStandingOrderUC standingOrderControl in standingOrderControls.Values)
            {
                wrapPanel.Children.Add(standingOrderControl);
            }
            return wrapPanel;
        }

        private void InitializeStandingOrderControls(ICollection<OrderVM> orders)
        {
            orders = InitializedOrders(orders);
            InitializeOrderEntries(orders);
            foreach (OrderVM order in orders)
            {
                standingOrderControls.Add(order.OrderId,
                    new PrintingStandingOrderUC(new PrintingStandingOrderViewModel(new StandingOrderUC(
                        new StandingOrderViewModel(order, eventAggregator, connectionString)))));
            }
        }

        private ICollection<OrderVM> InitializedOrders(ICollection<OrderVM> orders)
        {
            var factoryFloors = new FactoryFloorService(connectionString)
                .Find(orders.Select(o => o.FactoryFloorId)).ToDictionary(p => p.FactoryFloorId);
            var divisions = new DivisionService(connectionString)
                .Find(orders.Select(o => o.DivisionId)).ToDictionary(p => p.DivisionId);
            var parts = new PartService(connectionString)
                .Find(orders.Select(o => o.PartId)).ToDictionary(p => p.PartId);

            foreach (OrderVM order in orders)
            {
                order.Division = divisions[order.DivisionId];
                order.FactoryFloor = factoryFloors[order.FactoryFloorId];
                order.Part = parts[order.PartId];
            }
            return orders;
        }

        private void InitializeOrderEntries(ICollection<OrderVM> orders)
        {
            var orderEntries = orders.SelectMany(o => o.OrderEntries);
            var employees = new EmployeeService(connectionString).Find(orderEntries.Select(o => o.EmployeeId)).ToDictionary(p => p.EmployeeId);
            var partOperations = InitializedPartOperations(orderEntries).ToDictionary(p => p.PartOperationId);

            foreach (OrderVM order in orders)
            {
                foreach (OrderEntryVM orderEntry in order.OrderEntries)
                {
                    orderEntry.Employee = employees[orderEntry.EmployeeId];
                    orderEntry.PartOperation = partOperations[orderEntry.PartOperationId];
                }
            }
        }

        private ObservableCollection<PartOperationVM> InitializedPartOperations(IEnumerable<OrderEntryVM> orderEntries)
        {
            var partOperations = new PartOperationService(connectionString).Find(orderEntries.Select(o => o.PartOperationId));
            var operations = new OperationService(connectionString).Find(partOperations.Select(o => o.OperationId)).ToDictionary(p => p.OperationId);
            var wageRates = new WageRateService(connectionString).Find(partOperations.Select(o => o.WageRateId)).ToDictionary(p => p.WageRateId);

            foreach (PartOperationVM partOperation in partOperations)
            {
                partOperation.Operation = operations[partOperation.OperationId];
                partOperation.WageRateValue = wageRates[partOperation.WageRateId].WageRateValue;
            }
            return partOperations;
        }

        private void CheckAllOrdersToPrint()
        {
            foreach (PrintingStandingOrderUC printingStandingOrderUC in OrdersList.Children)
            {
                printingStandingOrderUC.ToPrintCheckBox.IsChecked = AllToPrint;
            }
        }

        public List<ContentControl> OrdersListToPrint()
        {
            var ordersListToPrint = new List<ContentControl>();

            foreach (PrintingStandingOrderUC printingStandingOrderUC in OrdersList.Children)
            {
                if (printingStandingOrderUC.ToPrintCheckBox.IsChecked.Value)
                {
                    ordersListToPrint.Add(printingStandingOrderUC.StandingOrderUC);
                }
            }
            return ordersListToPrint;
        }
        #endregion
    }
}
