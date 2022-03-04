using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace RatesFixer.BLL.Interfaces
{
    public interface IRangeFinderService<T>
    {
        ObservableCollection<T> Find(IEnumerable<int> idRange);
    }
}
