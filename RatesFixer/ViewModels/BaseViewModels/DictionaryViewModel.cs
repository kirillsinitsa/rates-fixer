using Prism.Events;
using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Models;
using RatesFixer.BLL.Services;
using System;
using RatesFixer.DAL.Interfaces;
using System.Threading;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RatesFixer.Infrastructure;

namespace RatesFixer.ViewModels.BaseViewModels
{
    public abstract class DictionaryViewModel<T, TService, TUnitOfWork> : BaseViewModel
        where T : ModelBase, new()
        where TUnitOfWork : IBaseUnitOfWork
        where TService : BaseService<TUnitOfWork>, IBaseService<T>
    {
        #region Properties
        protected readonly IEventAggregator eventAggregator;
        protected string connectionString;
        public bool IsAdmin => Thread.CurrentPrincipal.IsInRole("Administrators");
        protected TService itemService;
        public ObservableCollection<T> Items { get; protected set; }
        private T selectedItem = null;
        public T SelectedItem 
        { 
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
                OnPropertyChanged("IsItemSelected");
            }
        }
        public Action CloseAction { get; set; }
        public bool IsItemSelected => selectedItem != null;
        #endregion

        #region Commands
        private ICommand addItemCommand = null;

        public ICommand AddItemCommand => addItemCommand ?? (addItemCommand = new RelayCommand(AddItem));

        protected abstract void AddItem(object obj);

        private ICommand deleteItemCommand = null;

        public ICommand DeleteItemCommand => deleteItemCommand ?? (deleteItemCommand = new RelayCommand(DeleteItem));

        protected abstract void DeleteItem(object obj);

        private ICommand editItemCommand = null;

        public ICommand EditItemCommand => editItemCommand ?? (editItemCommand = new RelayCommand(EditItem));

        protected abstract void EditItem(object obj);
        #endregion
        #region Methods
        public void Close()
        {
            CloseAction();
        }

        protected void RefreshItems()
        {
            Items = itemService.GetAll();
        }
        #endregion

        #region Ctors
        public DictionaryViewModel(IEventAggregator eventAggregator, string connectionString)
        {
            this.connectionString = connectionString;
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<ApplicationClosedEvent>().Subscribe(Close);
        }
        #endregion
    }
}
