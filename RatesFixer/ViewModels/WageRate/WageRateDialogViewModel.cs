using RatesFixer.BLL.BusinessLogic;
using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Models;
using System.Collections.ObjectModel;
using RatesFixer.BLL.Services;
using RatesFixer.Infrastructure;
using RatesFixer.ViewModels.BaseViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RatesFixer.ViewModels.WageRate
{
    public class WageRateDialogViewModel : DialogViewModel<WageRateVM>
    {
        #region Fields and Properties
        private readonly string connectionString;
        private readonly IPartService partService;
        private readonly IPartOperationService partOperationService;
        private readonly IOperationService operationService;

        private ObservableCollection<PartVM> parts;
        public ObservableCollection<PartVM> Parts
        {
            get { return parts; }
            private set
            {
                parts = value;
                OnPropertyChanged();
            }
        }

        private PartVM selectedPart = null;
        public PartVM SelectedPart
        {
            get => selectedPart ?? (selectedPart = new PartVM());
            set
            {
                selectedPart = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PartOperationVM> partOperations;
        public ObservableCollection<PartOperationVM> PartOperations
        {
            get { return partOperations; }
            private set
            {
                partOperations = value;
                OnPropertyChanged();
            }
        }       
        #endregion
        #region Commands
        private ICommand calculateWageRateCommand = null;
        public ICommand CalculateWageRateCommand => calculateWageRateCommand ?? (calculateWageRateCommand = new RelayCommand(CalculateWageRate));
        private void CalculateWageRate(object obj)
        {
            try
            {
                CurrentItem = new WageRateCalculator(connectionString).Calculate(CurrentItem);
                OnPropertyChanged("CurrentItem");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка при расчете расценки");
            }

        }
        private ICommand loadPartOperationsCommand = null;

        public ICommand LoadPartOperationsCommand => loadPartOperationsCommand ?? (loadPartOperationsCommand = new RelayCommand(LoadPartOperations));

        private void LoadPartOperations(object parameter)
        {
            LoadPartOperations();
        }

        private void LoadPartOperations()
        {
            if (SelectedPart == null) return;
            try
            {                
                PartOperations = partOperationService.GetWhere(SelectedPart.PartId);               
                var operations = operationService.Find(PartOperations.Select(o => o.OperationId)).ToDictionary(p => p.OperationId);
                foreach (PartOperationVM partOperation in PartOperations)
                {
                    partOperation.Operation = operations[partOperation.OperationId];
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка получения данных операции по тех. процессу");
            }
        }

        private ICommand initializePartOperationCommand = null;

        public ICommand InitializePartOperationCommand => initializePartOperationCommand ?? (initializePartOperationCommand = new RelayCommand(InitializePartOperation));

        private void InitializePartOperation(object parameter)
        {
            if (CurrentItem.PartOperation == null) return;
            CurrentItem.PartOperation.Part = SelectedPart;
            CurrentItem.PartOperation.Operation = operationService.Get(CurrentItem.PartOperation.OperationId);
        }
        #endregion

        #region Ctors
        public WageRateDialogViewModel(WageRateVM wageRate, string connectionString) : base(wageRate)
        {
            this.connectionString = connectionString;           
            partOperationService = new PartOperationService(connectionString);
            operationService = new OperationService(connectionString);
            partService = new PartService(connectionString);
            Parts = partService.GetAll();
            if (CurrentItem.PartOperation != null)
            {
                SelectedPart = CurrentItem.PartOperation.Part;
                LoadPartOperations();
            }
        }
        #endregion
    }
}
