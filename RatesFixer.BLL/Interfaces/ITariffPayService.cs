using RatesFixer.BLL.Models;
using System.Collections.ObjectModel;
using RatesFixer.DAL.Enums;
using System.Collections.Generic;

namespace RatesFixer.BLL.Interfaces
{
    public interface ITariffPayService : IBaseService<TariffPayVM>
    {
        ObservableCollection<TariffPayVM> Find(int jobClass, int tariffPayGroupId, TariffPayType tariffPayType, int tariffPayCode);
        void UpdateCollection(ICollection<TariffPayVM> tariffPayVMs);
    }
}
