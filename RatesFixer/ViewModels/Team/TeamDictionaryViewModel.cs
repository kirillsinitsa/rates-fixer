using Prism.Events;
using RatesFixer.BLL.Infrastructure;
using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Models;
using RatesFixer.BLL.Services;
using RatesFixer.DAL.Repositories;
using RatesFixer.ErrorsHelper;
using RatesFixer.ViewModels.BaseViewModels;
using RatesFixer.Views.Team;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;

namespace RatesFixer.ViewModels.Team
{
    public class TeamDictionaryViewModel : DictionaryViewModel<TeamVM, TeamService, EFUnitOfWork>
    {
        private readonly IFactoryFloorService factoryFloorService;
        private readonly IDivisionService divisionService;
        private readonly IEmployeeService employeeService;
        #region Methods
        protected override void AddItem(object obj)
        {
            var addDialogVM = new TeamDialogViewModel(null, connectionString);
            var dialog = new TeamDialogWindow(addDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    new TeamService(connectionString).Create(addDialogVM.CurrentItem);
                    eventAggregator.GetEvent<TeamCreatedEvent>().Publish(addDialogVM.CurrentItem);
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
                int itemId = SelectedItem.TeamId;
                itemService.Delete(SelectedItem.DivisionId);
                eventAggregator.GetEvent<TeamDeletedEvent>().Publish(itemId);
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
            var editDialogVM = new TeamDialogViewModel(new TeamVM(SelectedItem), connectionString);
            var dialog = new TeamDialogWindow(editDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    itemService.Update(editDialogVM.CurrentItem);
                    eventAggregator.GetEvent<TeamChangedEvent>().Publish(editDialogVM.CurrentItem);
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

        private void RefreshTeams()
        {
            RefreshItems();
            try
            {
                var divisions = divisionService.Find(Items.Select(o => o.DivisionId)).ToDictionary(p => p.DivisionId);
                var factoryFloors = factoryFloorService.Find(divisions.Select(o => o.Value.FactoryFloorId)).ToDictionary(p => p.FactoryFloorId);
                var employees = employeeService.Find(Items.Select(o => o.ForemanId)).ToDictionary(p => p.EmployeeId);

                foreach (TeamVM item in Items)
                {
                    item.Division = divisions[item.DivisionId];
                    item.Division.FactoryFloor = factoryFloors[item.Division.FactoryFloorId];
                    item.Foreman = employees[item.ForemanId];
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка получения данных бригады");
            }
        }
        #endregion

        #region Ctors
        public TeamDictionaryViewModel(IEventAggregator eventAggregator, string connectionString) : base(eventAggregator, connectionString)
        {
            itemService = new TeamService(connectionString);
            divisionService = new DivisionService(connectionString);
            factoryFloorService = new FactoryFloorService(connectionString);
            employeeService = new EmployeeService(connectionString);
            RefreshTeams();
        }
        #endregion
    }
}
