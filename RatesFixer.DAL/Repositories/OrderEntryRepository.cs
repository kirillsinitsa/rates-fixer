using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Interfaces;
using RatesFixer.DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace RatesFixer.DAL.Repositories
{
    public class OrderEntryRepository : BaseRepository<OrderEntry, RatesContext>, IRepository<OrderEntry>
    {
        public OrderEntryRepository(RatesContext context) : base(context)
        {
            dbset = context.OrderEntries;
        }
        public override void DeleteRange(IEnumerable<int> idRange)
        {
            var entities = dbset.Where(o => idRange.Contains(o.OrderEntryId));

            dbset.RemoveRange(entities);
        }
    }
}
