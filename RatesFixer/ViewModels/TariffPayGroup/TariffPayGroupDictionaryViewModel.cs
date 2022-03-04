using Prism.Events;
using RatesFixer.BLL.Infrastructure;
using RatesFixer.BLL.Models;
using RatesFixer.BLL.Services;
using RatesFixer.DAL.Repositories;
using RatesFixer.ErrorsHelper;
using RatesFixer.ViewModels.BaseViewModels;
using RatesFixer.Views.TariffPayGroup;
using System;
using System.Data.Entity.Validation;
using System.Windows;

namespace RatesFixer.ViewModels.TariffPayGroup
{
    public class TariffPayGroupDictionaryViewModel : DictionaryViewModel<TariffPayGroupVM, TariffPayGroupService, EFUnitOfWork>
    {
        #region Methods
        protected override void AddItem(object obj)
        {
            var addDialogVM = new TariffPayGroupDialogViewModel(null);
            var dialog = new TariffPayGroupDialogWindow(addDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    new TariffPayGroupService(connectionString).Create(addDialogVM.CurrentItem);
                    eventAggregator.GetEvent<TariffPayGroupCreatedEvent>().Publish(addDialogVM.CurrentItem);
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
                int itemId = SelectedItem.TariffPayGroupId;
                itemService.Delete(itemId);                
                eventAggregator.GetEvent<TariffPayGroupDeletedEvent>().Publish(itemId);
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
            var editDialogVM = new TariffPayGroupDialogViewModel(new TariffPayGroupVM(SelectedItem));
            var dialog = new TariffPayGroupDialogWindow(editDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    itemService.Update(editDialogVM.CurrentItem);
                    eventAggregator.GetEvent<TariffPayGroupChangedEvent>().Publish(editDialogVM.CurrentItem);
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
        public TariffPayGroupDictionaryViewModel(IEventAggregator eventAggregator, string connectionString) : base(eventAggregator, connectionString)
        {
            itemService = new TariffPayGroupService(connectionString);
            RefreshItems();
        }
        #endregion
    }
}
