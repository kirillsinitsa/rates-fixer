using System.Collections.Generic;

namespace RatesFixer.Providers
{
    public class PaymentTypeProvider
    {
        public List<string> GetUsefulProductsPaymentTypes()
        {
            var paymentTypes = new List<string>() { "11", "103/104", "57/157" };
            return paymentTypes;
        }

        public List<string> GetDefectProductsPaymentTypes()
        {
            var paymentTypes = new List<string>() { "107" };
            return paymentTypes;
        }
    }
}
