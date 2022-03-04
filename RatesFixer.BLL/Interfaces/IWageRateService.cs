using RatesFixer.BLL.Models;

namespace RatesFixer.BLL.Interfaces
{
    public interface IWageRateService : IBaseService<WageRateVM>, IRangeFinderService<WageRateVM>
    {
    }
}
