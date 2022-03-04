﻿namespace RatesFixer.Authentication.Principal
{
    public class AnonymousIdentity : CustomIdentity
    {
        public AnonymousIdentity()
            : base(0, string.Empty)
        { }
    }
}
