using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Interfaces;
using RatesFixer.DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace RatesFixer.DAL.Repositories
{
    public class TeamRepository : BaseRepository<Team, RatesContext>, IRepository<Team>
    {
        public TeamRepository(RatesContext context) : base(context)
        {
            dbset = context.Teams;
        }

        public override void DeleteRange(IEnumerable<int> idRange)
        {
            var entities = dbset.Where(o => idRange.Contains(o.TeamId));

            dbset.RemoveRange(entities);
        }
    }
}
