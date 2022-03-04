using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Interfaces;
using RatesFixer.DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace RatesFixer.DAL.Repositories
{
    public class PartRepository : BaseRepository<Part, RatesContext>, IRepository<Part>
    {
        public PartRepository(RatesContext context) : base(context)
        {
            dbset = context.Parts;
        }

        public override void DeleteRange(IEnumerable<int> idRange)
        {
            var entities = dbset.Where(o => idRange.Contains(o.PartId));

            dbset.RemoveRange(entities);
        }
    }
}
