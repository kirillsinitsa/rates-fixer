using System;
using System.Collections.Generic;

namespace RatesFixer.Providers
{
    public class YearProvider
    {
        public List<int> GetYears()
        {
            var years = new List<int>();
            for (int i = 2020; i <= DateTime.Now.Year; i++)
            {
                years.Add(i);
            }
            return years;
        }
    }
}
