using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.Events;
using RateFixer.ViewModels.Authentication;
using RatesFixer.BLL.Infrastructure;
using RatesFixer.BLL.Models;
using RatesFixer.BLL.Services;
using RatesFixer.Infrastructure;
using RatesFixer.ViewModels.Authentication;
using RatesFixer.ViewModels.BaseViewModels;
using RatesFixer.ViewModels.Division;
using RatesFixer.ViewModels.Employee;
using RatesFixer.ViewModels.FactoryFloor;
using RatesFixer.ViewModels.Operation;
using RatesFixer.ViewModels.Part;
using RatesFixer.ViewModels.StandingOrder;
using RatesFixer.ViewModels.TariffMultiplier;
using RatesFixer.ViewModels.TariffPay;
using RatesFixer.ViewModels.TariffPayGroup;
using RatesFixer.ViewModels.Team;
using RatesFixer.ViewModels.WageRate;
using RatesFixer.ViewModels.WorkStation;
using RatesFixer.Views.Authentication;
using RatesFixer.Views.Division;
using RatesFixer.Views.Employee;
using RatesFixer.Views.FactoryFloor;
using RatesFixer.Views.Operation;
using RatesFixer.Views.Part;
using RatesFixer.Views.StandingOrder;
using RatesFixer.Views.TariffMultiplier;
using RatesFixer.Views.TariffPay;
using RatesFixer.Views.TariffPayGroup;
using RatesFixer.Views.Team;
using RatesFixer.Views.WageRate;
using RatesFixer.Views.WorkStation;

namespace RatesFixer.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Properties
        private readonly string connectionString;
        private readonly string credentialsConnectionString;
        private IEventAggregator eventAggregator;
        private StandingOrderViewerViewModel standingOrderViewerVM;
        private StandingOrderViewerUC standingOrderViewer;
        public StandingOrderViewerUC StandingOrderViewer
        {
            get => standingOrderViewer;
            set
            {
                standingOrderViewer = value;
                OnPropertyChanged();
            }
        }
        public bool IsAdmin => Thread.CurrentPrincipal.IsInRole("Administrators");

        public bool CanPrint { get; set; } = false;

        public bool CanCloseStandingOrders { get; set; } = false;

        public Action CloseAction { get; set; }
        #endregion

        #region Ctors
        public MainViewModel()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            credentialsConnectionString = ConfigurationManager.ConnectionStrings["CredentialsDefaultConnection"].ConnectionString;
            eventAggregator = ApplicationService.Instance.EventAggregator;
            eventAggregator.GetEvent<OrdersClosedEvent>().Subscribe(CloseStandingOrders);
        }
        #endregion

        #region Commands
        private RelayCommand closeCommand;
        public ICommand CloseCommand => closeCommand ?? (closeCommand = new RelayCommand(param => Close()));

        public void Close()
        {
            eventAggregator.GetEvent<ApplicationClosedEvent>().Publish();
            CloseAction();
        }
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            eventAggregator.GetEvent<ApplicationClosedEvent>().Publish();
        }
        public void OpenFactoryFloorDictionary_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void OpenFactoryFloorDictionary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = new FactoryFloorDictionaryViewModel(eventAggregator, connectionString);
            var view = new FactoryFloorDictionaryWindow(viewModel);
            view.Show();
        }
        public void OpenDivisionDictionary_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void OpenDivisionDictionary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = new DivisionDictionaryViewModel(eventAggregator, connectionString);
            var view = new DivisionDictionaryWindow(viewModel);
            view.Show();
        }
        public void OpenWorkStationDictionary_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void OpenWorkStationDictionary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = new WorkStationDictionaryViewModel(eventAggregator, connectionString);
            var view = new WorkStationDictionaryWindow(viewModel);
            view.Show();
        }
        public void OpenEmployeeDictionary_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void OpenEmployeeDictionary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = new EmployeeDictionaryViewModel(eventAggregator, connectionString);
            var view = new EmployeeDictionaryWindow(viewModel);
            view.Show();
        }
        public void OpenOperationDictionary_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void OpenOperationDictionary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = new OperationDictionaryViewModel(eventAggregator, connectionString);
            var view = new OperationDictionaryWindow(viewModel);
            view.Show();
        }
        public void OpenWageRateDictionary_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void OpenWageRateDictionary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = new WageRateDictionaryViewModel(eventAggregator, connectionString);
            var view = new WageRateDictionaryWindow(viewModel);
            view.Show();
        }
        public void OpenTariffMultiplierDictionary_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void OpenTariffMultiplierDictionary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = new TariffMultiplierDictionaryViewModel(eventAggregator, connectionString);
            var view = new TariffMultiplierDictionaryWindow(viewModel);
            view.Show();
        }
        public void OpenTariffPayGroupDictionary_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void OpenTariffPayGroupDictionary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = new TariffPayGroupDictionaryViewModel(eventAggregator, connectionString);
            var view = new TariffPayGroupDictionaryWindow(viewModel);
            view.Show();
        }
        public void OpenTariffPayDictionary_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void OpenTariffPayDictionary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = new TariffPayDictionaryViewModel(eventAggregator, connectionString);
            var view = new TariffPayDictionaryWindow(viewModel);
            view.Show();
        }
        public void OpenPartDictionary_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void OpenPartDictionary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = new PartDictionaryViewModel(eventAggregator, connectionString);
            var view = new PartDictionaryWindow(viewModel);
            view.Show();
        }

        public void OpenTeamDictionary_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void OpenTeamDictionary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = new TeamDictionaryViewModel(eventAggregator, connectionString);
            var view = new TeamDictionaryWindow(viewModel);
            view.Show();
        }

        public void OpenStandingOrderEditor_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void OpenStandingOrderEditor_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = new StandingOrderEditorViewModel(null, eventAggregator, connectionString);
            var view = new StandingOrderEditorWindow(viewModel);
            view.Show();
        }
        public void OpenUsersDictionary_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void OpenUsersDictionary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = new UsersViewModel(eventAggregator, credentialsConnectionString);
            var view = new UsersWindow(viewModel);
            view.Show();
        }

        public void OpenLogonWindow_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void OpenLogonWindow_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = new RegistrationViewModel(eventAggregator, credentialsConnectionString);
            var view = new RegistrationWindow(viewModel);
            view.Show();
        }

        public void OpenLoginWindow_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void OpenLoginWindow_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = new AuthenticationViewModel(eventAggregator, credentialsConnectionString);
            var view = new LoginWindow(viewModel);
            view.Show();
        }
        private ICommand viewStandingOrdersCommand = null;
        public ICommand ViewStandingOrdersCommand => viewStandingOrdersCommand ?? (viewStandingOrdersCommand = new RelayCommand(ViewStandingOrders));
        public void ViewStandingOrders(object sender)
        {
            ViewStandingOrders(new OrderService(connectionString).GetAll());
        }

        private ICommand selectStandingOrdersCommand;

        public ICommand SelectStandingOrdersCommand => selectStandingOrdersCommand ?? (selectStandingOrdersCommand = new RelayCommand(SelectStandingOrders));

        public void SelectStandingOrders(object sender)
        {
            var selectDialogVM = new StandingOrderSelectionViewModel(eventAggregator, connectionString);
            var dialog = new StandingOrderSelectionWindow(selectDialogVM);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                try
                {
                    ViewStandingOrders(selectDialogVM.Orders);
                }
                catch (ValidationException e)
                {
                    MessageBox.Show(e.Message);
                    NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка выбора рапортов о выработке");
                }
            }
        }
        public void ViewStandingOrders(ICollection<OrderVM> orders)
        {
            if (orders == null || orders.Count == 0) return;
            standingOrderViewerVM = new StandingOrderViewerViewModel(orders, eventAggregator, connectionString);
            StandingOrderViewer = new StandingOrderViewerUC(standingOrderViewerVM);
            CanCloseStandingOrders = true;
            OnPropertyChanged("CanCloseStandingOrders");
            CanPrint = true;
            OnPropertyChanged("CanPrint");
        }
        private ICommand closeStandingOrdersCommand = null;
        public ICommand CloseStandingOrdersCommand => closeStandingOrdersCommand ?? (closeStandingOrdersCommand = new RelayCommand(CloseStandingOrders));
        public void CloseStandingOrders(object obj)
        {
            CloseStandingOrders();
        }

        public void CloseStandingOrders()
        {
            StandingOrderViewer = null;
            standingOrderViewerVM = null;
            CanCloseStandingOrders = false;
            OnPropertyChanged("CanCloseStandingOrders");
            CanPrint = false;
            OnPropertyChanged("CanPrint");
        }
        private ICommand printStandingOrdersCommand = null;
        public ICommand PrintStandingOrdersCommand => printStandingOrdersCommand ?? (printStandingOrdersCommand = new RelayCommand(PrintStandingOrders));
        public void PrintStandingOrders(object obj)
        {
            PrintDialog printDlg = new PrintDialog();
            if (printDlg.ShowDialog() != true) return;
            var ordersList = standingOrderViewerVM.OrdersListToPrint();
            foreach (ContentControl standingOrder in ordersList)
            {
                printDlg.PrintVisual(standingOrder, "Печать наряда");
            }
        }
        #endregion
    }
}
