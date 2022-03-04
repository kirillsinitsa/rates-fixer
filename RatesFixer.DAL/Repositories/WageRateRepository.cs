using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Interfaces;
using RatesFixer.DAL.EF;

namespace RatesFixer.DAL.Repositories
{
    public class WageRateRepository : BaseRepository<WageRate, RatesContext>, IRepository<WageRate>
    {
        public WageRateRepository(RatesContext context) : base(context)
        {
            dbset = context.WageRates;
        }
    }
}
