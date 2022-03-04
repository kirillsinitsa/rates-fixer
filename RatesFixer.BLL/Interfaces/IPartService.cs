using RatesFixer.BLL.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace RatesFixer.BLL.Interfaces
{
    public interface IPartService : IBaseService<PartVM>
    {
        ObservableCollection<PartVM> Find(IEnumerable<int> idRange);
    }
}
