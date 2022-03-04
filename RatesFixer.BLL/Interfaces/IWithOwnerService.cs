using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace RatesFixer.BLL.Interfaces
{
    public interface IWithOwnerService<T>
    {
        ObservableCollection<T> GetWhere(int? ownerId);

        ObservableCollection<T> FindWhere(IEnumerable<int> ownersIdRange);
    }
}
