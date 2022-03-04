using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Interfaces;
using RatesFixer.DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace RatesFixer.DAL.Repositories
{
    public class WorkStationRepository : BaseRepository<WorkStation, RatesContext>, IRepository<WorkStation>
    {
        public WorkStationRepository(RatesContext context) : base(context)
        {
            dbset = context.WorkStations;
        }

        public override void DeleteRange(IEnumerable<int> idRange)
        {
            var entities = dbset.Where(o => idRange.Contains(o.WorkStationId));

            dbset.RemoveRange(entities);
        }
    }
}
