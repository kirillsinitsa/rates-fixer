using System.Security.Principal;

namespace RatesFixer.Authentication.Principal
{
    public class CustomIdentity : IIdentity
    {
        
        public CustomIdentity(int userId, string role)
        {
            UserId = userId;
            Role = role;
        }

        public int UserId { get; private set; }
        public string Name => UserId.ToString();
        public string Role { get; private set; }

        #region IIdentity Members
        public string AuthenticationType { get { return "Custom authentication"; } }

        public bool IsAuthenticated { get { return UserId != 0; } }
        #endregion
    }
}
