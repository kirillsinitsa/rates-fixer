using RatesFixer.DAL.Enums;
using System.Collections.Generic;

namespace RatesFixer.Providers
{
    public class MonthProvider
    {
        public List<Month> GetMonths()
        {
            var months = new List<Month>();
            for (int i = 1; i <= 12; i++)
            {
                months.Add((Month)i);
            }
            return months;
        }
    }
}
