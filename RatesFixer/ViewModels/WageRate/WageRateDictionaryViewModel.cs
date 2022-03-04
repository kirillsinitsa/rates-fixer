using Prism.Events;
using RatesFixer.BLL.Infrastructure;
using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Models;
using RatesFixer.BLL.Services;
using RatesFixer.DAL.Repositories;
using RatesFixer.ErrorsHelper;
using RatesFixer.ViewModels.BaseViewModels;
using RatesFixer.Views.WageRate;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;

namespace RatesFixer.ViewModels.WageRate
{
    public class WageRateDictionaryViewModel : DictionaryViewModel<WageRateVM, WageRateService, EFUnitOfWork>
    {
        #region Fields
        private readonly IPartService partService;
        private readonly IPartOperationService partOperationService;
        private readonly IOperationService operationService;
        #endregion
        #region Methods
        protected override void AddItem(object obj)
        {
            var addDialogVM = new WageRateDialogViewModel(null, connectionString);
            var dialog = new WageRateDialogWindow(addDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    itemService.Create(addDialogVM.CurrentItem);
                    var partOperation = partOperationService.Get(addDialogVM.CurrentItem.PartOperationId);
                    partOperation.WageRateId = itemService.GetIdWhere(partOperation.PartOperationId);
                    partOperationService.Update(partOperation);
                    Items.Add(addDialogVM.CurrentItem);
                    eventAggregator.GetEvent<WageRateCreatedEvent>().Publish(addDialogVM.CurrentItem);
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
                int itemId = SelectedItem.WageRateId;
                itemService.Delete(itemId);
                eventAggregator.GetEvent<WageRateDeletedEvent>().Publish(itemId);
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
            var editDialogVM = new WageRateDialogViewModel(new WageRateVM(SelectedItem), connectionString);
            var dialog = new WageRateDialogWindow(editDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    itemService.Update(editDialogVM.CurrentItem);
                    eventAggregator.GetEvent<WageRateChangedEvent>().Publish(editDialogVM.CurrentItem);
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

        protected void RefreshWageRates()
        {
            RefreshItems();
            try
            {
                var partOperations = partOperationService.GetAll().ToDictionary(p => p.PartOperationId);
                var parts = partService.GetAll().ToDictionary(p => p.PartId);
                var operations = operationService.GetAll().ToDictionary(p => p.OperationId);
                foreach (WageRateVM item in Items)
                {
                    item.PartOperation = partOperations[item.PartOperationId];
                    item.PartOperation.Part = parts[item.PartOperation.PartId];
                    item.PartOperation.Operation = operations[item.PartOperation.OperationId];
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка изменения данных расценки");
            }
        }
        #endregion

        #region Ctors
        public WageRateDictionaryViewModel(IEventAggregator eventAggregator, string connectionString) : base(eventAggregator, connectionString)
        {
            partOperationService = new PartOperationService(connectionString);
            partService = new PartService(connectionString);
            operationService = new OperationService(connectionString);
            itemService = new WageRateService(connectionString);
            RefreshWageRates();
        }
        #endregion
    }
}
