using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Interfaces;
using RatesFixer.DAL.EF;
using System.Linq;
using System.Collections.Generic;

namespace RatesFixer.DAL.Repositories
{
    public class FactoryFloorRepository : BaseRepository<FactoryFloor, RatesContext>, IRepository<FactoryFloor>
    {
        public FactoryFloorRepository(RatesContext context) : base(context)
        {
            dbset = context.FactoryFloors;
        }

        public override void DeleteRange(IEnumerable<int> idRange)
        {
            var entities = dbset.Where(o => idRange.Contains(o.FactoryFloorId));

            dbset.RemoveRange(entities);
        }
    }
}
