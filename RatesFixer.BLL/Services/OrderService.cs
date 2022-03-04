using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Infrastructure;
using RatesFixer.BLL.Models;
using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Repositories;
using System.Linq;
using System.Collections.ObjectModel;
using RatesFixer.BLL.Mappers;
using System.Collections.Generic;
using RatesFixer.DAL.Enums;

namespace RatesFixer.BLL.Services
{
    public class OrderService : BaseService<EFUnitOfWork>, IOrderService
    {
        public OrderService(string connectionString) : base(connectionString)
        { }

        public void Create(OrderVM orderVM)
        {
            var mapper = AutoMapperConfiguration.OrderVMToOrder.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                database.Orders.Create(mapper.Map<Order>(orderVM));
                database.Save();
                orderVM.OrderId = database.Orders.Find(o => o.Number == orderVM.Number).Last().OrderId;
            }
        }

        public OrderVM Get(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер наряда", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                var division = database.Orders.Get(id.Value);
                if (division == null) throw new ValidationException("Наряд не найден", "");
                var mapper = AutoMapperConfiguration.OrderToOrderVM.CreateMapper();
                return mapper.Map<OrderVM>(division);
            }
        }

        public ObservableCollection<OrderVM> GetAll()
        {
            var mapper = AutoMapperConfiguration.OrderToOrderVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<OrderVM>>(database.Orders.GetAll());
            }
        }
        public void Update(OrderVM orderVM)
        {
            var mapper = AutoMapperConfiguration.OrderVMToOrder.CreateMapper();
            var order = mapper.Map<Order>(orderVM);
            using (database = new EFUnitOfWork(connectionString))
            {
                database.Orders.Update(order);
                database.Save();
            }
        }

        public void Delete(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер наряда", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                database.Orders.Delete(id.Value);
                database.Save();
            }
        }

        public ObservableCollection<OrderVM> Select(int startYear, int endYear, Month startMonth, Month endMonth)
        {
            if (startYear == 0 || endYear == 0 || startMonth == 0 || endMonth == 0)
                throw new ValidationException("Не указан период", "");
            var mapper = AutoMapperConfiguration.OrderToOrderVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<OrderVM>>(database.Orders.Find
                    (o => o.Year >= startYear && o.Year <= endYear && o.Month >= startMonth && o.Month <= endMonth));
            }
        }
    }
}
