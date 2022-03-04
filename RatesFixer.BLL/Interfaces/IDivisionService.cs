using RatesFixer.BLL.Models;

namespace RatesFixer.BLL.Interfaces
{
    public interface IDivisionService : IBaseService<DivisionVM>, IWithOwnerService<DivisionVM>, IRangeFinderService<DivisionVM>
    {
    }
}
