using RatesFixer.ViewModels.PartOperation;
using System.Windows.Controls;

namespace RatesFixer.Views.PartOperation
{
    /// <summary>
    /// Логика взаимодействия для PartOperationUC.xaml
    /// </summary>
    public partial class PartOperationsUC : UserControl
    {
        public PartOperationsUC()
        {
            InitializeComponent();
        }
        public PartOperationsUC(PartOperationsDictionaryViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}

