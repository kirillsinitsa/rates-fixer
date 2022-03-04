using Prism.Events;
using RatesFixer.BLL.Infrastructure;
using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Models;
using RatesFixer.BLL.Services;
using RatesFixer.DAL.Repositories;
using RatesFixer.ErrorsHelper;
using RatesFixer.ViewModels.BaseViewModels;
using RatesFixer.Views.Division;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;

namespace RatesFixer.ViewModels.Division
{
    public class DivisionDictionaryViewModel : DictionaryViewModel<DivisionVM, DivisionService, EFUnitOfWork>
    {
        private readonly IFactoryFloorService factoryFloorService;
        #region Methods
        protected override void AddItem(object obj)
        {
            var addDialogVM = new DivisionDialogViewModel(null, connectionString);
            var dialog = new DivisionDialogWindow(addDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    itemService.Create(addDialogVM.CurrentItem);
                    eventAggregator.GetEvent<DivisionCreatedEvent>().Publish(addDialogVM.CurrentItem);
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
                int itemId = SelectedItem.DivisionId;
                itemService.Delete(itemId);
                eventAggregator.GetEvent<DivisionDeletedEvent>().Publish(itemId);
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
            var editDialogVM = new DivisionDialogViewModel(new DivisionVM(SelectedItem), connectionString);
            var dialog = new DivisionDialogWindow(editDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    itemService.Update(editDialogVM.CurrentItem);
                    eventAggregator.GetEvent<DivisionChangedEvent>().Publish(editDialogVM.CurrentItem);
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

        private void RefreshDivisions()
        {
            RefreshItems();
            try
            {
                var factoryFloors = factoryFloorService.GetAll().ToDictionary(o => o.FactoryFloorId);

                foreach (DivisionVM item in Items)
                {
                    item.FactoryFloor = factoryFloors[item.FactoryFloorId];
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка получения данных цеха");
            }
        }
        #endregion

        #region Ctors
        public DivisionDictionaryViewModel(IEventAggregator eventAggregator, string connectionString) : base(eventAggregator, connectionString)
        {           
            itemService = new DivisionService(connectionString);
            factoryFloorService = new FactoryFloorService(connectionString);
            RefreshDivisions();
        }
        #endregion
    }
}
