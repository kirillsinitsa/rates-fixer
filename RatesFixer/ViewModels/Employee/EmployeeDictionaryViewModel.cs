using Prism.Events;
using RatesFixer.BLL.Infrastructure;
using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Models;
using RatesFixer.BLL.Services;
using RatesFixer.DAL.Repositories;
using RatesFixer.ErrorsHelper;
using RatesFixer.ViewModels.BaseViewModels;
using RatesFixer.Views.Employee;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;

namespace RatesFixer.ViewModels.Employee
{
    public class EmployeeDictionaryViewModel : DictionaryViewModel<EmployeeVM, EmployeeService, EFUnitOfWork>
    {
        private readonly IFactoryFloorService factoryFloorService;
        private readonly IDivisionService divisionService;
        private readonly IWorkStationService workStationService;
        #region Methods
        protected override void AddItem(object obj)
        {
            var addDialogVM = new EmployeeDialogViewModel(null, connectionString);
            var dialog = new EmployeeDialogWindow(addDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    new EmployeeService(connectionString).Create(addDialogVM.CurrentItem);
                    eventAggregator.GetEvent<EmployeeCreatedEvent>().Publish(addDialogVM.CurrentItem);
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
                int itemId = SelectedItem.EmployeeId;
                itemService.Delete(itemId);
                eventAggregator.GetEvent<EmployeeDeletedEvent>().Publish(itemId);
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
            var editDialogVM = new EmployeeDialogViewModel(new EmployeeVM(SelectedItem), connectionString);
            var dialog = new EmployeeDialogWindow(editDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    itemService.Update(editDialogVM.CurrentItem);
                    eventAggregator.GetEvent<EmployeeChangedEvent>().Publish(editDialogVM.CurrentItem);
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
        private void RefreshEmployees()
        {
            RefreshItems();
            try
            {
                var workStations = workStationService.Find(Items.Select(o => o.WorkStationId)).ToDictionary(p => p.WorkStationId);
                var divisions = divisionService.Find(workStations.Select(o => o.Value.DivisionId)).ToDictionary(p => p.DivisionId);
                var factoryFloors = factoryFloorService.Find(divisions.Select(o => o.Value.FactoryFloorId)).ToDictionary(p => p.FactoryFloorId);

                foreach (EmployeeVM item in Items)
                {
                    item.WorkStation = workStations[item.WorkStationId];
                    item.WorkStation.Division = divisions[item.WorkStation.DivisionId];
                    item.WorkStation.Division.FactoryFloor = factoryFloors[item.WorkStation.Division.FactoryFloorId];
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
        public EmployeeDictionaryViewModel(IEventAggregator eventAggregator, string connectionString) : base(eventAggregator, connectionString)
        {
            itemService = new EmployeeService(connectionString);
            divisionService = new DivisionService(connectionString);
            factoryFloorService = new FactoryFloorService(connectionString);
            workStationService = new WorkStationService(connectionString);
            RefreshEmployees();
        }
        #endregion
    }
}
