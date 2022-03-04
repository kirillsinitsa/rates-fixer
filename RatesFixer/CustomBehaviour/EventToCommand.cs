using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace RatesFixer.CustomBehaviour
{
    public class EventToCommand : TriggerAction<FrameworkElement>, ICommandSource
    {
        public static readonly DependencyProperty EventArgsProperty = DependencyProperty.Register(
            "EventArgs",
            typeof(object),
            typeof(EventToCommand));

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            "CommandParameter",
            typeof(object),
            typeof(EventToCommand),
            new PropertyMetadata(
                null,
                (s, e) =>
                {
                    var sender = s as EventToCommand;
                    if (sender == null)
                    {
                        return;
                    }

                    if (sender.AssociatedObject == null)
                    {
                        return;
                    }

                    sender.EnableDisableElement();
                }));

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(EventToCommand),
            new PropertyMetadata(
                null,
                (s, e) => OnCommandChanged(s as EventToCommand, e)));

        public static readonly DependencyProperty MustToggleIsEnabledProperty = DependencyProperty.Register(
            "MustToggleIsEnabled",
            typeof(bool),
            typeof(EventToCommand),
            new PropertyMetadata(
                false,
                (s, e) =>
                {
                    var sender = s as EventToCommand;
                    if (sender == null)
                    {
                        return;
                    }

                    if (sender.AssociatedObject == null)
                    {
                        return;
                    }

                    sender.EnableDisableElement();
                }));

        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }

            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public object EventArgs
        {
            get
            {
                return GetValue(EventArgsProperty);
            }

            set
            {
                SetValue(EventArgsProperty, value);
            }
        }

        public object CommandParameter
        {
            get
            {
                return this.GetValue(CommandParameterProperty);
            }

            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        public IInputElement CommandTarget
        {
            get { return this.AssociatedObject; }
        }

        public bool MustToggleIsEnabled
        {
            get
            {
                return (bool)this.GetValue(MustToggleIsEnabledProperty);
            }

            set
            {
                SetValue(MustToggleIsEnabledProperty, value);
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            EnableDisableElement();
        }

        public void Invoke()
        {
            Invoke(null);
        }

        protected override void Invoke(object parameter)
        {

            EventArgs = parameter;

            if (AssociatedElementIsDisabled())
            {
                return;
            }

            if (CanExecute())
                Execute();
        }

        private static void OnCommandChanged(
            EventToCommand element,
            DependencyPropertyChangedEventArgs e)
        {
            if (element == null)
            {
                return;
            }

            if (e.OldValue != null)
            {
                ((ICommand)e.OldValue).CanExecuteChanged -= element.OnCommandCanExecuteChanged;
            }

            var command = (ICommand)e.NewValue;

            if (command != null)
            {
                command.CanExecuteChanged += element.OnCommandCanExecuteChanged;
            }

            element.EnableDisableElement();
        }

        private bool AssociatedElementIsDisabled()
        {

            return AssociatedObject != null && !AssociatedObject.IsEnabled;
        }

        private void EnableDisableElement()
        {

            if (AssociatedObject == null)
            {
                return;
            }


            if (MustToggleIsEnabled && Command != null)
            {
                AssociatedObject.IsEnabled = CanExecute();
            }
        }

        private void OnCommandCanExecuteChanged(object sender, EventArgs e)
        {
            EnableDisableElement();
        }

        public void Execute()
        {
            if (Command == null)
                return;

            var routedCommand = Command as RoutedCommand;
            if (routedCommand != null)
                routedCommand.Execute(CommandParameter, CommandTarget);
            else
                Command.Execute(CommandParameter);
        }

        public bool CanExecute()
        {
            if (Command == null)
                return false;

            var routedCommand = Command as RoutedCommand;
            if (routedCommand != null)
                return routedCommand.CanExecute(CommandParameter, CommandTarget);

            return Command.CanExecute(CommandParameter);
        }
    }
}
