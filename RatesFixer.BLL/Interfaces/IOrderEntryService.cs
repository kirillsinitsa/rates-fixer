using RatesFixer.BLL.Models;

namespace RatesFixer.BLL.Interfaces
{
    public interface IOrderEntryService : IBaseService<OrderEntryVM>, IWithOwnerService<OrderEntryVM>, IRangeFinderService<OrderEntryVM>
    {
        void DeleteWhere(int? orderId);
    }
}
