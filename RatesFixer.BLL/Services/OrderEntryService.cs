using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Infrastructure;
using RatesFixer.BLL.Models;
using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using RatesFixer.BLL.Mappers;

namespace RatesFixer.BLL.Services
{
    public class OrderEntryService : BaseService<EFUnitOfWork>, IOrderEntryService
    {
        public OrderEntryService(string connectionString) : base(connectionString)
        { }

        public void Create(OrderEntryVM orderEntryVM)
        {
            var mapper = AutoMapperConfiguration.OrderEntryVMToOrderEntry.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                if (database.Orders.Get(orderEntryVM.OrderId) == null)
                    throw new ValidationException("Наряд не найден", "");
                database.OrderEntries.Create(mapper.Map<OrderEntry>(orderEntryVM));
                database.Save();
                orderEntryVM.OrderEntryId = database.OrderEntries.Find(o => o.OrderId == orderEntryVM.OrderId).Last().OrderId;
            }
        }

        public OrderEntryVM Get(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер позиции наряда", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                var division = database.OrderEntries.Get(id.Value);
                if (division == null)
                    throw new ValidationException("Позиция наряда не найдена", "");
                var mapper = AutoMapperConfiguration.OrderEntryToOrderEntryVM.CreateMapper();
                return mapper.Map<OrderEntryVM>(division);
            }
        }

        public ObservableCollection<OrderEntryVM> GetAll()
        {
            var mapper = AutoMapperConfiguration.OrderEntryToOrderEntryVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<OrderEntryVM>>(database.OrderEntries.GetAll());
            }
        }
        public void Update(OrderEntryVM orderEntryVM)
        {
            var mapper = AutoMapperConfiguration.OrderEntryVMToOrderEntry.CreateMapper();
            var division = mapper.Map<OrderEntry>(orderEntryVM);
            using (database = new EFUnitOfWork(connectionString))
            {
                database.OrderEntries.Update(division);
                database.Save();
            }
        }

        public void Delete(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер позиции наряда", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                database.OrderEntries.Delete(id.Value);
                database.Save();
            }
        }

        public ObservableCollection<OrderEntryVM> GetWhere(int? orderId)
        {
            if (orderId == null) throw new ValidationException("Не указан номер наряда", "");
            var mapper = AutoMapperConfiguration.OrderEntryToOrderEntryVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<OrderEntryVM>>
                    (database.OrderEntries.Find(o => o.OrderId == orderId.Value));
            }
        }

        public void DeleteWhere(int? orderId)
        {
            if (orderId == null) throw new ValidationException("Не указан номер наряда", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                var orderEntries = database.OrderEntries.Find(o => o.OrderId == orderId.Value);
                foreach (OrderEntry orderEntry in orderEntries)
                {
                    database.OrderEntries.Delete(orderEntry.OrderEntryId);
                }
                database.Save();
            }
        }
        public ObservableCollection<OrderEntryVM> FindWhere(IEnumerable<int> ordersIdRange)
        {
            var mapper = AutoMapperConfiguration.OrderEntryToOrderEntryVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<OrderEntryVM>>
                    (database.OrderEntries.Find(o => ordersIdRange.Contains(o.OrderId)));
            }
        }
        public ObservableCollection<OrderEntryVM> Find(IEnumerable<int> idRange)
        {
            var mapper = AutoMapperConfiguration.OrderEntryToOrderEntryVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<OrderEntryVM>>
                    (database.OrderEntries.Find(o => idRange.Contains(o.OrderEntryId)));
            }
        }
    }
}
