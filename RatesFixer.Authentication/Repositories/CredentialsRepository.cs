using RatesFixer.Authentication.Entities;
using RatesFixer.DAL.Interfaces;
using RatesFixer.Authentication.EF;
using RatesFixer.DAL.Repositories;

namespace RatesFixer.Authentication.Repositories
{
    public class CredentialsRepository : BaseRepository<InternalUserData, CredentialsContext>, IRepository<InternalUserData>
    {
        public CredentialsRepository(CredentialsContext context) : base(context)
        {
            dbset = context.Credentials;
        }
    }
}
