using RatesFixer.BLL.BusinessLogic;
using RatesFixer.BLL.Models;
using System.Collections.ObjectModel;
using RatesFixer.Infrastructure;
using RatesFixer.ViewModels.BaseViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace RatesFixer.ViewModels.TariffPay
{
    public class TariffPayDialogViewModel : DialogViewModel<CalculateTariffPayVM>
    {
        #region Fields and Properties
        private readonly TariffPayCalculator tariffPayCalculator;
        public ObservableCollection<TariffPayVM> TariffPays { get; set; }
        #endregion

        #region Commands
        private ICommand calculateTariffPaysCommand = null;
        public ICommand CalculateTariffPaysCommand => calculateTariffPaysCommand ?? (calculateTariffPaysCommand = new RelayCommand(CalculateTariffPays));
        private void CalculateTariffPays(object obj)
        {
            try 
            {
                TariffPays = tariffPayCalculator.Calculate(CurrentItem.AnnualOperationalHours,
                                                           CurrentItem.FirstClassTimeWorkPay,
                                                           CurrentItem.FirstClassPieceWorkPay);
                MessageBox.Show("Тарифные ставки пересчитаны.");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка при расчете тарифной ставки");
            }           
        }
        #endregion

        #region Ctors
        public TariffPayDialogViewModel(string connectionString) : base(null)
        {
            tariffPayCalculator = new TariffPayCalculator(connectionString);
            TariffPays = new ObservableCollection<TariffPayVM>();
        }
        #endregion
    }
}
