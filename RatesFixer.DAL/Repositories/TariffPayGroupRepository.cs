using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Interfaces;
using RatesFixer.DAL.EF;

namespace RatesFixer.DAL.Repositories
{
    public class TariffPayGroupRepository : BaseRepository<TariffPayGroup, RatesContext>, IRepository<TariffPayGroup>
    {
        public TariffPayGroupRepository(RatesContext context) : base(context)
        {
            dbset = context.TariffPayGroups;
        }
    }
}