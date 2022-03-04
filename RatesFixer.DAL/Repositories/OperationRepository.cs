using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Interfaces;
using RatesFixer.DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace RatesFixer.DAL.Repositories
{
    public class OperationRepository : BaseRepository<Operation, RatesContext>, IRepository<Operation>
    {
        public OperationRepository(RatesContext context) : base(context)
        {
            dbset = context.Operations;
        }

        public override void DeleteRange(IEnumerable<int> idRange)
        {
            var entities = dbset.Where(o => idRange.Contains(o.OperationId));

            dbset.RemoveRange(entities);
        }
    }
}
