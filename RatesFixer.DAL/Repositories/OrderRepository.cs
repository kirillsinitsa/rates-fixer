using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Interfaces;
using RatesFixer.DAL.EF;
using System.Collections.Generic;
using System;
using System.Linq;

namespace RatesFixer.DAL.Repositories
{
    public class OrderRepository : BaseRepository<Order, RatesContext>, IRepository<Order>
    {
        public OrderRepository(RatesContext context) : base(context)
        {
            dbset = context.Orders;
        }
        public override Order Get(int id)
        {
            var order = dbset.Find(id);
            context.Entry(order).Collection(o => o.OrderEntries).Load();
            return order;
        }
        public override IEnumerable<Order> Find(Func<Order, bool> predicate)
        {
            var orders = dbset.Where(predicate);           
            return LoadOrderEntries(orders);
        }

        private IEnumerable<Order> LoadOrderEntries(IEnumerable<Order> orders)
        {
            foreach (Order order in orders)
            {
                context.Entry(order).Collection(o => o.OrderEntries).Load();
            }
            return orders;
        }
        public override IEnumerable<Order> GetAll()
        {
            return LoadOrderEntries(dbset);
        }
    }
}
