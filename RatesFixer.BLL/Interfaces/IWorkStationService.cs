using RatesFixer.BLL.Models;

namespace RatesFixer.BLL.Interfaces
{
    public interface IWorkStationService : IBaseService<WorkStationVM>, IWithOwnerService<WorkStationVM>, IRangeFinderService<WorkStationVM>
    {
    }
}
