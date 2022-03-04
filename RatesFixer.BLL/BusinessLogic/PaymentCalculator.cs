using RatesFixer.BLL.Models;

namespace RatesFixer.BLL.BusinessLogic
{
    public class PaymentCalculator
    {
        public double Sum(int productsCount, double wageRateValue)
        {
            return productsCount * wageRateValue;
        }

        public double StandardHours(int productsCount, double rateTime)
        {
            return productsCount * rateTime / 60;
        }

        public void CalculatePayment(OrderEntryVM orderEntry)
        {
            CalculateStandardHours(orderEntry);
            CalculateSums(orderEntry);
        }

        public void CalculateStandardHours(OrderEntryVM orderEntry)
        {
            orderEntry.UsefulProductsPayment.StandardHours = StandardHours(orderEntry.UsefulProducts, orderEntry.PartOperation.RateTime);
            orderEntry.DefectProductsPayment.StandardHours = StandardHours(orderEntry.PayedDefectProducts, orderEntry.PartOperation.RateTime);
        }

        public void CalculateSums(OrderEntryVM orderEntry)
        {
            orderEntry.UsefulProductsPayment.Sum = Sum(orderEntry.UsefulProducts, orderEntry.PartOperation.WageRateValue);
            orderEntry.DefectProductsPayment.Sum = Sum(orderEntry.PayedDefectProducts, orderEntry.PartOperation.WageRateValue);
        }
    }
}
