using RatesFixer.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RatesFixer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var viewModel = new MainViewModel();

            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(() => this.Close());
            Closing += viewModel.OnWindowClosing;

            var openFactoryFloorDictionaryCommandBinding = new CommandBinding(
                                Commands.OpenFactoryFloorDictionaryCommand,
                                viewModel.OpenFactoryFloorDictionary_Executed,
                                viewModel.OpenFactoryFloorDictionary_CanExecute);
            var openDivisionDictionaryCommandBinding = new CommandBinding(
                                Commands.OpenDivisionDictionaryCommand,
                                viewModel.OpenDivisionDictionary_Executed,
                                viewModel.OpenDivisionDictionary_CanExecute);
            var openWorkStationDictionaryCommandBinding = new CommandBinding(
                                Commands.OpenWorkStationDictionaryCommand,
                                viewModel.OpenWorkStationDictionary_Executed,
                                viewModel.OpenWorkStationDictionary_CanExecute);
            var openEmployeeDictionaryCommandBinding = new CommandBinding(
                                Commands.OpenEmployeeDictionaryCommand,
                                viewModel.OpenEmployeeDictionary_Executed,
                                viewModel.OpenEmployeeDictionary_CanExecute);
            var openOperationDictionaryCommandBinding = new CommandBinding(
                                Commands.OpenOperationDictionaryCommand,
                                viewModel.OpenOperationDictionary_Executed,
                                viewModel.OpenOperationDictionary_CanExecute);
            var openWageRateDictionaryCommandBinding = new CommandBinding(
                               Commands.OpenWageRateDictionaryCommand,
                               viewModel.OpenWageRateDictionary_Executed,
                               viewModel.OpenWageRateDictionary_CanExecute);
            var openTariffMultiplierDictionaryCommandBinding = new CommandBinding(
                                Commands.OpenTariffMultiplierDictionaryCommand,
                                viewModel.OpenTariffMultiplierDictionary_Executed,
                                viewModel.OpenTariffMultiplierDictionary_CanExecute);
            var openTariffPayGroupDictionaryCommandBinding = new CommandBinding(
                                Commands.OpenTariffPayGroupDictionaryCommand,
                                viewModel.OpenTariffPayGroupDictionary_Executed,
                                viewModel.OpenTariffPayGroupDictionary_CanExecute);
            var openTariffPayDictionaryCommandBinding = new CommandBinding(
                                Commands.OpenTariffPayDictionaryCommand,
                                viewModel.OpenTariffPayDictionary_Executed,
                                viewModel.OpenTariffPayDictionary_CanExecute);
            var openPartDictionaryCommandBinding = new CommandBinding(
                                Commands.OpenPartDictionaryCommand,
                                viewModel.OpenPartDictionary_Executed,
                                viewModel.OpenPartDictionary_CanExecute);
            var openTeamDictionaryCommandBinding = new CommandBinding(
                                Commands.OpenTeamDictionaryCommand,
                                viewModel.OpenTeamDictionary_Executed,
                                viewModel.OpenTeamDictionary_CanExecute);
            var openStandingOrderEditorCommandBinding = new CommandBinding(
                                Commands.OpenStandingOrderEditorCommand,
                                viewModel.OpenStandingOrderEditor_Executed,
                                viewModel.OpenStandingOrderEditor_CanExecute);
            var openLoginWindowCommandBinding = new CommandBinding(
                                Commands.OpenLoginWindowCommand,
                                viewModel.OpenLoginWindow_Executed,
                                viewModel.OpenLoginWindow_CanExecute);
            var openLogonWindowCommandBinding = new CommandBinding(
                                Commands.OpenLogonWindowCommand,
                                viewModel.OpenLogonWindow_Executed,
                                viewModel.OpenLogonWindow_CanExecute);
            var openUsersDictionaryCommandBinding = new CommandBinding(
                                Commands.OpenUsersDictionaryCommand,
                                viewModel.OpenUsersDictionary_Executed,
                                viewModel.OpenUsersDictionary_CanExecute);

            DataContext = viewModel;

            CommandBindings.Add(openFactoryFloorDictionaryCommandBinding);
            CommandBindings.Add(openDivisionDictionaryCommandBinding);
            CommandBindings.Add(openWorkStationDictionaryCommandBinding);
            CommandBindings.Add(openEmployeeDictionaryCommandBinding);
            CommandBindings.Add(openOperationDictionaryCommandBinding);
            CommandBindings.Add(openWageRateDictionaryCommandBinding);
            CommandBindings.Add(openTariffMultiplierDictionaryCommandBinding);
            CommandBindings.Add(openTariffPayGroupDictionaryCommandBinding);
            CommandBindings.Add(openTariffPayDictionaryCommandBinding);
            CommandBindings.Add(openPartDictionaryCommandBinding);
            CommandBindings.Add(openTeamDictionaryCommandBinding);
            CommandBindings.Add(openStandingOrderEditorCommandBinding);
            CommandBindings.Add(openLoginWindowCommandBinding);
            CommandBindings.Add(openLogonWindowCommandBinding);
            CommandBindings.Add(openUsersDictionaryCommandBinding);
        }
    }
}
