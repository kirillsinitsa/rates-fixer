using System.Data.Entity;
using RatesFixer.Authentication.Entities;

namespace RatesFixer.Authentication.EF
{
    public class CredentialsContext : DbContext
    {       
        public DbSet<InternalUserData> Credentials { get; set; }

        public CredentialsContext(string connectionString) : base(connectionString) { }
    }
}
