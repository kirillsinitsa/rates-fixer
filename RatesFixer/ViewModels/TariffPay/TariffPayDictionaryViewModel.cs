using Prism.Events;
using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Models;
using System.Collections.ObjectModel;
using RatesFixer.BLL.Services;
using RatesFixer.Infrastructure;
using RatesFixer.ViewModels.BaseViewModels;
using RatesFixer.Views.TariffPay;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Data.Entity.Validation;
using RatesFixer.BLL.Infrastructure;
using RatesFixer.ErrorsHelper;
using RatesFixer.BLL.BusinessLogic;

namespace RatesFixer.ViewModels.TariffPay
{
    public class TariffPayDictionaryViewModel : BaseViewModel
    {
        #region Fields and Properties
        private string connectionString;
        private IEventAggregator eventAggregator;
        private ITariffPayService tariffPayService;
        private ObservableCollection<TariffPayVM> tariffPays;
        public ObservableCollection<TariffPayVM> TariffPays
        {
            get { return tariffPays; }
            private set
            {
                tariffPays = value;
                OnPropertyChanged();
            }
        }
        public Action CloseAction { get; set; }
        #endregion

        #region Commands
        private ICommand openDialogCommand = null;

        public ICommand OpenDialogCommand => openDialogCommand ?? (openDialogCommand = new RelayCommand(OpenDialog));

        private void OpenDialog(object obj)
        {
            var addDialogVM = new TariffPayDialogViewModel(connectionString);
            var dialog = new TariffPayDialogWindow(addDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                TariffPays = addDialogVM.TariffPays;
                tariffPayService.UpdateCollection(TariffPays);
                new WageRateCalculator(connectionString).UpdateAllWageRates();
                RefreshTariffPays();
                eventAggregator.GetEvent<TariffPaysChangedEvent>().Publish(TariffPays.ToList());
            }
        }
        #endregion

        #region Methods
        public void Close()
        {
            CloseAction();
        }
        private void RefreshTariffPays()
        {
            var tariffMultipliers = new TariffMultiplierService(connectionString).GetAll().ToDictionary(o => o.JobClass);
            foreach (TariffPayVM tariffPay in TariffPays)
                try
                {
                    tariffPay.TariffMultiplier = tariffMultipliers[tariffPay.JobClass].Multiplier;
                }
                catch (DbEntityValidationException e)
                {
                    MessageBox.Show(e.EntityValidationErrorsToString());
                }
                catch (ValidationException e)
                {
                    MessageBox.Show(e.Message);
                    NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка добавления рапорта о выработке");
                }
        }
        #endregion

        #region Ctors
        public TariffPayDictionaryViewModel(IEventAggregator eventAggregator, string connectionString)
        {
            this.connectionString = connectionString;
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<ApplicationClosedEvent>().Subscribe(Close);
            tariffPayService = new TariffPayService(connectionString);          
            TariffPays = tariffPayService.GetAll();
            RefreshTariffPays();
        }
        #endregion
    }
}
