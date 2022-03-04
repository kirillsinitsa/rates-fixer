using RatesFixer.Authentication.Entities;
using RatesFixer.DAL.Interfaces;

namespace RatesFixer.Authentication.Interfaces
{
    interface ICredentialsUnitOfWork : IBaseUnitOfWork
    {
        IRepository<InternalUserData> Credentials { get; }
    }
}
