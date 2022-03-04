using Prism.Events;
using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Models;
using System.Collections.ObjectModel;
using RatesFixer.BLL.Services;
using RatesFixer.Infrastructure;
using RatesFixer.ViewModels.BaseViewModels;
using RatesFixer.Views.Part;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using RatesFixer.DAL.Repositories;
using System.Data.Entity.Validation;
using RatesFixer.BLL.Infrastructure;
using RatesFixer.ErrorsHelper;

namespace RatesFixer.ViewModels.Part
{
    public class PartDictionaryViewModel : DictionaryViewModel<PartVM, PartService, EFUnitOfWork>
    {
        #region Fields and Properties
        private readonly IFactoryFloorService factoryFloorService;
        private readonly IDivisionService divisionService;
        private readonly IOperationService operationService;
        private readonly IPartOperationService partOperationService;

        private ObservableCollection<PartOperationVM> selectedPartOperations;
        public ObservableCollection<PartOperationVM> SelectedPartOperations
        {
            get { return selectedPartOperations; }
            private set
            {
                selectedPartOperations = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Methods
        protected override void AddItem(object obj)
        {
            var addDialogVM = new PartDialogViewModel(eventAggregator, connectionString, null);
            var dialog = new PartDialogWindow(addDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    new PartService(connectionString).Create(addDialogVM.CurrentItem);
                    eventAggregator.GetEvent<PartChangedEvent>().Publish(addDialogVM.CurrentItem);
                    Items.Add(addDialogVM.CurrentItem);
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
        }

        protected override void DeleteItem(object obj)
        {
            var res = MessageBox.Show("Вы уверены?", "Удаление", MessageBoxButton.OKCancel);
            if (res == MessageBoxResult.Cancel) return;
            try
            {
                int itemId = SelectedItem.PartId;
                itemService.Delete(itemId);
                eventAggregator.GetEvent<PartDeletedEvent>().Publish(itemId);
                Items.Remove(SelectedItem);
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

        protected override void EditItem(object obj)
        {
            var editDialogVM = new PartDialogViewModel(eventAggregator, connectionString, SelectedItem);
            var dialog = new PartDialogWindow(editDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    itemService.Update(editDialogVM.CurrentItem);
                    eventAggregator.GetEvent<PartChangedEvent>().Publish(editDialogVM.CurrentItem);
                    Items[Items.IndexOf(SelectedItem)] = editDialogVM.CurrentItem;
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
        }
        #endregion

        #region Ctors
        public PartDictionaryViewModel(IEventAggregator eventAggregator, string connectionString) : base(eventAggregator, connectionString)
        {
            partOperationService = new PartOperationService(connectionString);
            divisionService = new DivisionService(connectionString);
            factoryFloorService = new FactoryFloorService(connectionString);
            operationService = new OperationService(connectionString);
            itemService = new PartService(connectionString);
            Items = itemService.GetAll();
        }
        #endregion

        #region Commands
        private ICommand loadPartOperationsCommand = null;

        public ICommand LoadPartOperationsCommand => loadPartOperationsCommand ?? (loadPartOperationsCommand = new RelayCommand(LoadPartOperations));

        private void LoadPartOperations(object parameter)
        {
            if (SelectedItem != null)
            {
                SelectedPartOperations = partOperationService.GetWhere(SelectedItem.PartId);               
                var divisions = divisionService.Find(SelectedPartOperations.Select(o => o.DivisionId)).ToDictionary(p => p.DivisionId);
                var factoryFloors = factoryFloorService.Find(divisions.Select(o => o.Value.FactoryFloorId)).ToDictionary(p => p.FactoryFloorId);
                var operations = operationService.Find(SelectedPartOperations.Select(o => o.OperationId)).ToDictionary(p => p.OperationId);
                foreach (PartOperationVM item in SelectedPartOperations)
                {
                    item.Operation = operations[item.OperationId];
                    item.Division = divisions[item.DivisionId];
                    item.Division.FactoryFloor = factoryFloors[item.Division.FactoryFloorId];
                }
            }
        }
        #endregion
    }
}
