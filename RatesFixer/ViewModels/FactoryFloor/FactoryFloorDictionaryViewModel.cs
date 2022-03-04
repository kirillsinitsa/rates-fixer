using Prism.Events;
using RatesFixer.BLL.Infrastructure;
using RatesFixer.BLL.Models;
using RatesFixer.BLL.Services;
using RatesFixer.DAL.Repositories;
using RatesFixer.ErrorsHelper;
using RatesFixer.ViewModels.BaseViewModels;
using RatesFixer.Views.FactoryFloor;
using System;
using System.Data.Entity.Validation;
using System.Windows;

namespace RatesFixer.ViewModels.FactoryFloor
{
    public class FactoryFloorDictionaryViewModel : DictionaryViewModel<FactoryFloorVM, FactoryFloorService, EFUnitOfWork>
    {
        #region Methods
        protected override void AddItem(object obj)
        {
            var addDialogVM = new FactoryFloorDialogViewModel(null);
            var dialog = new FactoryFloorDialogWindow(addDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    new FactoryFloorService(connectionString).Create(addDialogVM.CurrentItem);                   
                    eventAggregator.GetEvent<FactoryFloorCreatedEvent>().Publish(addDialogVM.CurrentItem);
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
            var res = MessageBox.Show(string.Format("Удалить цех {0}?", SelectedItem.Name), "Удаление", MessageBoxButton.OKCancel);
            if (res == MessageBoxResult.Cancel) return;
            try
            {
                int itemId = SelectedItem.FactoryFloorId;
                itemService.Delete(itemId);
                eventAggregator.GetEvent<FactoryFloorDeletedEvent>().Publish(itemId);
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
            var editDialogVM = new FactoryFloorDialogViewModel(new FactoryFloorVM(SelectedItem));
            var dialog = new FactoryFloorDialogWindow(editDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    itemService.Update(editDialogVM.CurrentItem);
                    eventAggregator.GetEvent<FactoryFloorChangedEvent>().Publish(editDialogVM.CurrentItem);
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
        public FactoryFloorDictionaryViewModel(IEventAggregator eventAggregator, string connectionString) : base(eventAggregator, connectionString)
        {
            itemService = new FactoryFloorService(connectionString);
            Items = itemService.GetAll();
        }
        #endregion
    }
}
