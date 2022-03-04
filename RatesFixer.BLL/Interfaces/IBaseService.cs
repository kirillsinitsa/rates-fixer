using System.Collections.ObjectModel;

namespace RatesFixer.BLL.Interfaces
{
    public interface IBaseService<T>
    {
        ObservableCollection<T> GetAll();
        T Get(int? id);
        void Create(T item);
        void Delete(int? id);
        void Update(T item);
    }
}
