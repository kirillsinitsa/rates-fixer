using RatesFixer.BLL.Models;
using RatesFixer.Infrastructure;
using System.Windows;
using System.Windows.Input;

namespace RatesFixer.ViewModels.BaseViewModels
{
    public class DialogViewModel<T> : BaseViewModel where T : ModelBase, new()
    {
        private T _currentItem = null;

        public T CurrentItem
        {
            get => _currentItem ?? (_currentItem = new T());
            set => _currentItem = value;
        }

        private ICommand okCommand = null;

        public ICommand OkCommand => okCommand ?? (okCommand = new RelayCommand(Ok, o => !CurrentItem.HasErrors));

        private void Ok(object parameter)
        {
            if (parameter is Window dialogWindow)
            {
                dialogWindow.DialogResult = true;
                dialogWindow.Close();
            }
        }

        public DialogViewModel(T currentItem)
        {
            _currentItem = currentItem;
        }
    }
}
