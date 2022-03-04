using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Models;
using System.Collections.ObjectModel;
using RatesFixer.BLL.Services;
using RatesFixer.DAL.Enums;
using System;

namespace RatesFixer.BLL.BusinessLogic
{
    public class TariffPayCalculator
    {
        private readonly ITariffPayService tariffPayService;
        private readonly string connectionString;
        private double hourMultiplier;
        public TariffPayCalculator(string connectionString)
        {
            this.connectionString = connectionString;
            tariffPayService = new TariffPayService(connectionString);           
        }
        private void Populate()
        {
            for (int groupId = 1; groupId <= 3; groupId++)
                for (int code = 17; code <= 18; code++)
                    for (TariffPayType type = TariffPayType.Month; type <= TariffPayType.Minute; type++)
                        for (int jobClass = 1; jobClass <= 6; jobClass++)
                        {
                            tariffPayService.Create(new TariffPayVM { JobClass = jobClass,
                                                                      TariffPayGroupId = groupId,
                                                                      TariffPayCode = code - 2 * groupId,
                                                                      TariffPayType = type });
                        }
        }

        public ObservableCollection<TariffPayVM> Calculate(int annualOperationalHours, double firstClassTimeWorkPay, double firstClassPieceWorkPay)
        {           
            var tariffPays = GetTariffPays();
            var tariffPayGroups = new TariffPayGroupService(connectionString).GetAll();
            var tariffMultipliers = new TariffMultiplierService(connectionString).GetAll();
            double tariffPayValue;

            SetHourMultiplier(annualOperationalHours);
            foreach (TariffPayVM tariffPay in tariffPays)
            {
                tariffPayValue = tariffPay.TariffPayCode % 2 == 0 ? firstClassPieceWorkPay : firstClassTimeWorkPay;
                tariffPayValue *= TariffPayTypeMultiplier(tariffPay.TariffPayType);
                tariffPayValue *= tariffPayGroups[tariffPay.TariffPayGroupId - 1].Multiplier;
                tariffPayValue *= tariffMultipliers[tariffPay.JobClass - 1].Multiplier;
                tariffPay.TariffPayValue = TariffPayRound(tariffPay.TariffPayType, tariffPayValue);
            }
            return tariffPays;
        }

        private ObservableCollection<TariffPayVM> GetTariffPays()
        {
            var tariffPays = tariffPayService.GetAll();
            foreach (TariffPayVM tariffPay in tariffPays)
            {
                tariffPayService.Delete(tariffPay.TariffPayId);
            }
            Populate();
            tariffPays = tariffPayService.GetAll();
            return tariffPays;
        }
        private void SetHourMultiplier(int annualOperationalHours)
        {
            if (annualOperationalHours == 0)
            {
                hourMultiplier = 1;
                throw new ArgumentException("Не указан годовой фонд времени.");
            }
            hourMultiplier = 1 / Math.Round((double)annualOperationalHours / 12, 1);
        }
        private double TariffPayTypeMultiplier(TariffPayType tariffPayType)
        {
            double multiplier = 1;
            switch (tariffPayType)
            {
                case TariffPayType.Hour:
                    multiplier = hourMultiplier;
                    break;
                case TariffPayType.Minute:
                    multiplier = hourMultiplier / 60;
                    break;
            }               
            return multiplier;
        }
        private double TariffPayRound(TariffPayType tariffPayType, double tariffPayValue)
        {
            switch (tariffPayType)
            {
                case TariffPayType.Month:
                    tariffPayValue = Math.Round(tariffPayValue, 2);
                    break;
                case TariffPayType.Hour:
                    tariffPayValue = Math.Round(tariffPayValue, 3);
                    break;
                case TariffPayType.Minute:
                    tariffPayValue = Math.Round(tariffPayValue, 4);
                    break;
            }
            return tariffPayValue;
        }
    }
}
