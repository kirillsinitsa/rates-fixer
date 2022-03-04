using Prism.Events;
using RatesFixer.BLL.Infrastructure;
using RatesFixer.BLL.Models;
using RatesFixer.BLL.Services;
using RatesFixer.DAL.Repositories;
using RatesFixer.ErrorsHelper;
using RatesFixer.ViewModels.BaseViewModels;
using RatesFixer.Views.Operation;
using System;
using System.Data.Entity.Validation;
using System.Windows;

namespace RatesFixer.ViewModels.Operation
{
    public class OperationDictionaryViewModel : DictionaryViewModel<OperationVM, OperationService, EFUnitOfWork>
    {
        #region Methods
        protected override void AddItem(object obj)
        {
            var addDialogVM = new OperationDialogViewModel(null);
            var dialog = new OperationDialogWindow(addDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    new OperationService(connectionString).Create(addDialogVM.CurrentItem);
                    eventAggregator.GetEvent<OperationCreatedEvent>().Publish(addDialogVM.CurrentItem);
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
                int itemId = SelectedItem.OperationId;
                itemService.Delete(itemId);
                eventAggregator.GetEvent<OperationDeletedEvent>().Publish(itemId);
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
            var editDialogVM = new OperationDialogViewModel(new OperationVM(SelectedItem));
            var dialog = new OperationDialogWindow(editDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    itemService.Update(editDialogVM.CurrentItem);
                    eventAggregator.GetEvent<OperationChangedEvent>().Publish(editDialogVM.CurrentItem);
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
        public OperationDictionaryViewModel(IEventAggregator eventAggregator, string connectionString) : base(eventAggregator, connectionString)
        {
            itemService = new OperationService(connectionString);
            Items = itemService.GetAll();
        }
        #endregion
    }
}
