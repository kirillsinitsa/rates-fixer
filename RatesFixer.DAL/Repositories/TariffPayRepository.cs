using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Interfaces;
using RatesFixer.DAL.EF;

namespace RatesFixer.DAL.Repositories
{
    public class TariffPayRepository : BaseRepository<TariffPay, RatesContext>, IRepository<TariffPay>
    {
        public TariffPayRepository(RatesContext context) : base(context)
        {
            dbset = context.TariffPays;
        }
    }
}
