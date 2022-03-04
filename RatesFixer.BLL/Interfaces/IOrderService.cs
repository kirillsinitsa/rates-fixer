using RatesFixer.BLL.Models;
using RatesFixer.DAL.Enums;
using System.Collections.ObjectModel;

namespace RatesFixer.BLL.Interfaces
{
    public interface IOrderService : IBaseService<OrderVM>
    {
        ObservableCollection<OrderVM> Select(int startYear, int endYear, Month startMonth, Month endMonth);
    }
}
