using System.Windows.Input;

namespace RatesFixer
{
    public static class Commands
    {
        public static readonly RoutedUICommand OpenFactoryFloorDictionaryCommand = new RoutedUICommand();
        public static readonly RoutedUICommand OpenDivisionDictionaryCommand = new RoutedUICommand();
        public static readonly RoutedUICommand OpenWorkStationDictionaryCommand = new RoutedUICommand();
        public static readonly RoutedUICommand OpenEmployeeDictionaryCommand = new RoutedUICommand();
        public static readonly RoutedUICommand OpenTeamDictionaryCommand = new RoutedUICommand();
        public static readonly RoutedUICommand OpenOperationDictionaryCommand = new RoutedUICommand();
        public static readonly RoutedUICommand OpenWageRateDictionaryCommand = new RoutedUICommand();
        public static readonly RoutedUICommand OpenTariffMultiplierDictionaryCommand = new RoutedUICommand();
        public static readonly RoutedUICommand OpenTariffPayGroupDictionaryCommand = new RoutedUICommand();
        public static readonly RoutedUICommand OpenTariffPayDictionaryCommand = new RoutedUICommand();
        public static readonly RoutedUICommand OpenPartDictionaryCommand = new RoutedUICommand();
        public static readonly RoutedUICommand OpenPartOperationDictionaryCommand = new RoutedUICommand();
        public static readonly RoutedUICommand OpenStandingOrderEditorCommand = new RoutedUICommand();
        public static readonly RoutedUICommand OpenUsersDictionaryCommand = new RoutedUICommand();
        public static readonly RoutedUICommand OpenLogonWindowCommand = new RoutedUICommand();
        public static readonly RoutedUICommand OpenLoginWindowCommand = new RoutedUICommand();
        public static readonly RoutedCommand ConfirmPasswordCommand = new RoutedCommand();
    }
}
