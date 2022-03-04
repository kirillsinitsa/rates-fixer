using System;

namespace RatesFixer.DAL.Interfaces
{
    public interface IBaseUnitOfWork : IDisposable
    {
        void Save();
    }
}
