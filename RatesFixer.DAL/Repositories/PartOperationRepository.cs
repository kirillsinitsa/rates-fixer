using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Interfaces;
using RatesFixer.DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace RatesFixer.DAL.Repositories
{
    public class PartOperationRepository : BaseRepository<PartOperation, RatesContext>, IRepository<PartOperation>
    {
        public PartOperationRepository(RatesContext context) : base(context)
        {
            dbset = context.PartOperations;
        }
        public override void DeleteRange(IEnumerable<int> idRange)
        {
            var entities = dbset.Where(o => idRange.Contains(o.PartOperationId));

            dbset.RemoveRange(entities);
        }
    }
}
