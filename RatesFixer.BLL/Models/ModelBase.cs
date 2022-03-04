using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RatesFixer.BLL.Models
{
    public abstract class ModelBase : INotifyDataErrorInfo, INotifyPropertyChanged
    {
        protected string[] GetErrorsFromAnnotations<T>(string propertyName, T value)
        {
            var results = new List<ValidationResult>();
            var vc = new ValidationContext(this, null, null) { MemberName = propertyName };
            var isValid = Validator.TryValidateProperty(value, vc, results);
            return (isValid) ? null : Array.ConvertAll(results.ToArray(), o => o.ErrorMessage);
        }

        # region INotifyDataErrorInfo members
        protected readonly Dictionary<string, List<string>> _errors =
            new Dictionary<string, List<string>>();

        protected void ClearErrors(string propertyName = "")
        {
            _errors.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

        protected void AddError(string propertyName, string error)
        {
            AddErrors(propertyName, new List<string> { error });
        }

        protected void AddErrors(string propertyName, IList<string> errors)
        {
            var changed = false;
            if (!_errors.ContainsKey(propertyName))
            {
                _errors.Add(propertyName, new List<string>());
                changed = true;
            }
            errors.ToList().ForEach(x =>
            {
                if (_errors[propertyName].Contains(x)) return;
                _errors[propertyName].Add(x);
                changed = true;
            });
            if (changed)
            {
                OnErrorsChanged(propertyName);
            }
        }
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return _errors;
            }
            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
        }
        public bool HasErrors => _errors.Count != 0;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnErrorsChanged([CallerMemberName] string propertyName = "")
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        #endregion

        # region INotifyPropertyChanged members
        internal virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
