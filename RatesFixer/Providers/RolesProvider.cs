using System.Collections.Generic;

namespace RatesFixer.Providers
{
    public class RolesProvider
    {
        public List<string> GetRoles()
        {
            return new List<string>() { "Administrators", "Users" };
        }
    }
}
