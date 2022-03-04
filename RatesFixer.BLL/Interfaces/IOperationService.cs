using RatesFixer.BLL.Models;

namespace RatesFixer.BLL.Interfaces
{
    public interface IOperationService : IBaseService<OperationVM>, IRangeFinderService<OperationVM>
    {
    }
}
