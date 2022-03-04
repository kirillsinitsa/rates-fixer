using Prism.Events;
using RatesFixer.BLL.Infrastructure;
using RatesFixer.BLL.Models;
using RatesFixer.BLL.Services;
using RatesFixer.ErrorsHelper;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;

namespace RatesFixer.Views.StandingOrder
{
    public class StandingOrderHandler
    {
        private readonly string connectionString;
        private readonly IEventAggregator eventAggregator;
        public StandingOrderHandler(IEventAggregator eventAggregator, string connectionString)
        {
            this.eventAggregator = eventAggregator;
            this.connectionString = connectionString;
        }

        public void AddOrder(OrderVM order)
        {
            try
            {
                CreateOrder(order);
                eventAggregator.GetEvent<OrderCreatedEvent>().Publish(order);
            }
            catch (DbEntityValidationException e)
            {
                MessageBox.Show(e.EntityValidationErrorsToString());
                throw;
            }
            catch (ValidationException e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка добавления рапорта о выработке");
            }        
        }

        private void CreateOrder(OrderVM order)
        {
            try
            {
                new OrderService(connectionString).Create(order);
                MessageBox.Show("Рапорт о выработке внесен в базу.");
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

        public void EditOrder(OrderVM order, OrderVM originalOrder)
        {
            try
            {
                UpdateOrder(order, originalOrder);
                MessageBox.Show("Изменения в рапорте о выработке внесены в базу.");
                eventAggregator.GetEvent<OrderChangedEvent>().Publish(order);
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

        private void UpdateOrder(OrderVM order, OrderVM originalOrder)
        {
            if (originalOrder == null) return;
            if (order != originalOrder) new OrderService(connectionString).Update(order);
            if (order.OrderEntries != originalOrder.OrderEntries) UpdateOrderEntries(order, originalOrder);
        }

        private void CreateOrderEntries(OrderVM order)
        {
            var orderEntryService = new OrderEntryService(connectionString);
            try
            {
                foreach (OrderEntryVM orderEntry in order.OrderEntries)
                {
                    if (orderEntry.OrderId == 0) orderEntry.OrderId = order.OrderId;
                    if (orderEntry.OrderEntryId == 0) orderEntryService.Create(orderEntry);
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

        private void UpdateOrderEntries(OrderVM order, OrderVM originalOrder)
        {
            CreateOrderEntries(order);
            var orderEntryService = new OrderEntryService(connectionString);
            try
            {
                var orderEntries = order.OrderEntries.ToDictionary(o => o.OrderEntryId);
                var originalOrderEntries = originalOrder.OrderEntries.ToDictionary(o => o.OrderEntryId);

                foreach (OrderEntryVM orderEntry in originalOrder.OrderEntries)
                {
                    if (!orderEntries.ContainsKey(orderEntry.OrderEntryId)) orderEntryService.Delete(orderEntry.OrderEntryId);
                }
                foreach (OrderEntryVM orderEntry in order.OrderEntries)
                {
                    if (originalOrderEntries.ContainsKey(orderEntry.OrderEntryId) && orderEntry != originalOrderEntries[orderEntry.OrderEntryId])
                    {
                        orderEntryService.Update(orderEntry);
                    }
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


        public void DeleteOrder(OrderVM order)
        {
            var res = MessageBox.Show("Вы уверены?", "Удаление", MessageBoxButton.OKCancel);
            if (res == MessageBoxResult.Cancel) return;
            try
            {
                int orderId = order.OrderId;
                new OrderService(connectionString).Delete(order.OrderId);
                eventAggregator.GetEvent<OrderDeletedEvent>().Publish(orderId);
            }
            catch (DbEntityValidationException e)
            {
                MessageBox.Show(e.EntityValidationErrorsToString());
            }
            catch (ValidationException e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, string.Format("Ошибка удаления рапорта о выработке id={0}", order.OrderId));
            }
        }
    }
}
