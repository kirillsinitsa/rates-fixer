using Prism.Events;
using RatesFixer.BLL.Infrastructure;
using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Models;
using RatesFixer.BLL.Services;
using RatesFixer.DAL.Repositories;
using RatesFixer.ErrorsHelper;
using RatesFixer.ViewModels.BaseViewModels;
using RatesFixer.Views.PartOperation;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;

namespace RatesFixer.ViewModels.PartOperation
{
    public class PartOperationsDictionaryViewModel : DictionaryViewModel<PartOperationVM, PartOperationService, EFUnitOfWork>
    {
        #region Fields and Properties
        private readonly IFactoryFloorService factoryFloorService;
        private readonly IDivisionService divisionService;
        private readonly IOperationService operationService;
        private int partId;
        public bool PartAdded => (partId != 0);
        #endregion
        #region Methods
        protected override void AddItem(object obj)
        {
            var addDialogVM = new PartOperationDialogViewModel(null, connectionString);
            var dialog = new PartOperationDialogWindow(addDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    addDialogVM.CurrentItem.PartId = partId;
                    new PartOperationService(connectionString).Create(addDialogVM.CurrentItem);
                    eventAggregator.GetEvent<PartOperationCreatedEvent>().Publish(addDialogVM.CurrentItem);
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
                int itemId = SelectedItem.PartOperationId;
                itemService.Delete(itemId);
                eventAggregator.GetEvent<PartOperationDeletedEvent>().Publish(itemId);
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
            var editDialogVM = new PartOperationDialogViewModel(new PartOperationVM(SelectedItem), connectionString);
            var dialog = new PartOperationDialogWindow(editDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    itemService.Update(editDialogVM.CurrentItem);
                    eventAggregator.GetEvent<PartOperationChangedEvent>().Publish(editDialogVM.CurrentItem);
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
        private void RefreshPartOperations()
        {
            if (partId == 0) return;
            try
            {
                Items = itemService.GetWhere(partId);
                var divisions = divisionService.Find(Items.Select(o => o.DivisionId)).ToDictionary(p => p.DivisionId);
                var factoryFloors = factoryFloorService.Find(divisions.Select(o => o.Value.FactoryFloorId)).ToDictionary(p => p.FactoryFloorId);
                var operations = operationService.Find(Items.Select(o => o.OperationId)).ToDictionary(p => p.OperationId);

                foreach (PartOperationVM item in Items)
                {
                    item.Operation = operations[item.OperationId];
                    item.Division = divisions[item.DivisionId];
                    item.Division.FactoryFloor = factoryFloors[item.Division.FactoryFloorId];
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка получения данных операции по тех. процессу");
            }
        }
        #endregion

        #region Ctors
        public PartOperationsDictionaryViewModel(IEventAggregator eventAggregator, string connectionString, int partId) : base(eventAggregator, connectionString)
        {
            itemService = new PartOperationService(connectionString);
            divisionService = new DivisionService(connectionString);
            factoryFloorService = new FactoryFloorService(connectionString);
            operationService = new OperationService(connectionString);
            this.partId = partId;
            RefreshPartOperations();
        }
        #endregion
    }
}

