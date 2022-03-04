using RatesFixer.DAL.Interfaces;

namespace RatesFixer.BLL.Services
{
    public abstract class BaseService<TUnitOfWork> where TUnitOfWork : IBaseUnitOfWork
    {
        protected TUnitOfWork database;
        protected string connectionString;
        public BaseService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Dispose()
        {
            database.Dispose();
        }
    }
}
