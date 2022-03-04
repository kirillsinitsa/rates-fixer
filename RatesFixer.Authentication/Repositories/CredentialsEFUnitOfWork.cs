using System;
using RatesFixer.Authentication.EF;
using RatesFixer.Authentication.Entities;
using RatesFixer.Authentication.Interfaces;
using RatesFixer.DAL.Interfaces;

namespace RatesFixer.Authentication.Repositories
{
    public class CredentialsEFUnitOfWork : ICredentialsUnitOfWork
    {
        private CredentialsContext db;
        private CredentialsRepository credentialsRepository;
        public CredentialsEFUnitOfWork(string connectionString)
        {
            db = new CredentialsContext(connectionString);
        }

        public IRepository<InternalUserData> Credentials
        {
            get
            {
                if (credentialsRepository == null)
                    credentialsRepository = new CredentialsRepository(db);
                return credentialsRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
