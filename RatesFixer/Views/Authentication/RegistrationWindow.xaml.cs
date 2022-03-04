using RatesFixer.ViewModels.Authentication;
using System;
using System.Windows;
using System.Windows.Input;

namespace RatesFixer.Views.Authentication
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }
        public RegistrationWindow(RegistrationViewModel viewModel) : this()
        {
            var confirmPasswordCommandBinding = new CommandBinding(
                                Commands.ConfirmPasswordCommand,
                                viewModel.ConfirmPassword_Execute,
                                viewModel.ConfirmPassword_CanExecute);

            DataContext = viewModel;

            CommandBindings.Add(confirmPasswordCommandBinding);

            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(() => this.Close());
        }

    }
}
