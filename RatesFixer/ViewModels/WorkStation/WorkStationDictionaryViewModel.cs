using Prism.Events;
using RatesFixer.BLL.Infrastructure;
using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Models;
using RatesFixer.BLL.Services;
using RatesFixer.DAL.Repositories;
using RatesFixer.ErrorsHelper;
using RatesFixer.ViewModels.BaseViewModels;
using RatesFixer.Views.WorkStation;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;

namespace RatesFixer.ViewModels.WorkStation
{
    public class WorkStationDictionaryViewModel : DictionaryViewModel<WorkStationVM, WorkStationService, EFUnitOfWork>
    {
        private readonly IFactoryFloorService factoryFloorService;
        private readonly IDivisionService divisionService;
        #region Methods
        protected override void AddItem(object obj)
        {
            var addDialogVM = new WorkStationDialogViewModel(null, connectionString);
            var dialog = new WorkStationDialogWindow(addDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    new WorkStationService(connectionString).Create(addDialogVM.CurrentItem);
                    eventAggregator.GetEvent<WorkStationCreatedEvent>().Publish(addDialogVM.CurrentItem);
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
                int itemId = SelectedItem.WorkStationId;
                itemService.Delete(itemId);
                eventAggregator.GetEvent<WorkStationDeletedEvent>().Publish(itemId);
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
            var editDialogVM = new WorkStationDialogViewModel(new WorkStationVM(SelectedItem), connectionString);
            var dialog = new WorkStationDialogWindow(editDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    itemService.Update(editDialogVM.CurrentItem);
                    eventAggregator.GetEvent<WorkStationChangedEvent>().Publish(editDialogVM.CurrentItem);
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

        private void RefreshWorkStations()
        {
            RefreshItems();
            try
            {
                var divisions = divisionService.Find(Items.Select(o => o.DivisionId)).ToDictionary(p => p.DivisionId);
                var factoryFloors = factoryFloorService.Find(divisions.Select(o => o.Value.FactoryFloorId)).ToDictionary(p => p.FactoryFloorId);

                foreach (WorkStationVM item in Items)
                {
                    item.Division = divisions[item.DivisionId];
                    item.Division.FactoryFloor = factoryFloors[item.Division.FactoryFloorId];
                }
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
        public WorkStationDictionaryViewModel(IEventAggregator eventAggregator, string connectionString) : base(eventAggregator, connectionString)
        {
            itemService = new WorkStationService(connectionString);
            divisionService = new DivisionService(connectionString);
            factoryFloorService = new FactoryFloorService(connectionString);
            RefreshWorkStations();
        }
        #endregion
    }
}
