using RatesFixer.BLL.Interfaces;
using RatesFixer.Authentication.Entities;
using RatesFixer.Authentication.Model;
using System.Collections.ObjectModel;

namespace RatesFixer.Authentication.Interfaces
{
    public interface ICredentialsService : IBaseService<InternalUserData>
    {
        ObservableCollection<UserVM> GetAllUsers();
    }
}
