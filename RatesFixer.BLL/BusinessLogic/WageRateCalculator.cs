using RatesFixer.BLL.Models;
using RatesFixer.BLL.Services;
using RatesFixer.DAL.Enums;
using System;
using System.Linq;
using System.Windows;

namespace RatesFixer.BLL.BusinessLogic
{
    public class WageRateCalculator
    {
        private string connectionString;
        public WageRateCalculator(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public WageRateVM Calculate(WageRateVM wageRate)
        {
            try
            {
                var operation = new OperationService(connectionString).Get(wageRate.PartOperation.OperationId);
                int tariffPayCode = 18 - 2 * operation.TariffPayGroupId;
                var minuteTariffPay = new TariffPayService(connectionString).Find(operation.JobClass, operation.TariffPayGroupId, TariffPayType.Minute, tariffPayCode).FirstOrDefault();
                var wageRateValue = minuteTariffPay.TariffPayValue * wageRate.PartOperation.RateTime * wageRate.PartOperation.Percentage * (1 + wageRate.IntencityOfWork / 100);
                wageRate.WageRateValue = Math.Round(wageRateValue, 4);            
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка получения данных операции по тех. процессу");
            }
            return wageRate;
        }

        public void UpdateAllWageRates()
        {
            var partOperations = new PartOperationService(connectionString).GetAll().ToDictionary(o => o.PartOperationId);
          //  var tariffMultipliers = new TariffMultiplierService(connectionString).GetAll().ToDictionary(o => o.JobClass);
            var wageRateService = new WageRateService(connectionString);
            var wageRates = wageRateService.GetAll();

            foreach (WageRateVM wageRate in wageRates)
            {
                wageRate.PartOperation = partOperations[wageRate.PartOperationId];
                wageRateService.Update(Calculate(wageRate));
            }
        }
    }
}
