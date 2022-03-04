using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Interfaces;
using RatesFixer.DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace RatesFixer.DAL.Repositories
{
    public class DivisionRepository : BaseRepository<Division, RatesContext>, IRepository<Division>
    {
        public DivisionRepository(RatesContext context) : base(context)
        {
            dbset = context.Divisions;
        }
        public override void DeleteRange(IEnumerable<int> idRange)
        {
            var entities = dbset.Where(o => idRange.Contains(o.DivisionId));

            dbset.RemoveRange(entities);
        }
    }
}
