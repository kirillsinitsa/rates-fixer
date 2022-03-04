using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Interfaces;
using RatesFixer.DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace RatesFixer.DAL.Repositories
{
    public class TariffMultiplierRepository : BaseRepository<TariffMultiplier, RatesContext>, IRepository<TariffMultiplier>
    {
        public TariffMultiplierRepository(RatesContext context) : base(context)
        {
            dbset = context.TariffMultipliers;
        }

        public override void DeleteRange(IEnumerable<int> idRange)
        {
            var entities = dbset.Where(o => idRange.Contains(o.JobClass));

            dbset.RemoveRange(entities);
        }
    }
}
