using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.BLL.Models
{
    public class FactoryFloorVM : ModelBase, IDataErrorInfo
    {
        #region Fields and Properties
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 100)]
        public int FactoryFloorId { get; set; }

        private string name;
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(50, ErrorMessage = "Длина не должна превышать 50 символов.")]
        public string Name 
        { 
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Ctors
        public FactoryFloorVM() { }
        public FactoryFloorVM(FactoryFloorVM other) : this()
        {
            FactoryFloorId = other.FactoryFloorId;
            Name = other.Name;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region IDataErrorInfo members
        public string this[string columnName]
        {
            get
            {
                string[] errors = null;
                bool hasError = false;
                switch (columnName)
                {
                    case nameof(FactoryFloorId):
                        errors = GetErrorsFromAnnotations(nameof(FactoryFloorId), FactoryFloorId);
                        break;
                    case nameof(Name):
                        errors = GetErrorsFromAnnotations(nameof(Name), Name);
                        break;
                }
                if (errors != null && errors.Length != 0)
                {
                    ClearErrors(columnName);
                    AddErrors(columnName, errors);
                    hasError = true;
                }
                if (!hasError) ClearErrors(columnName);
                return string.Empty;
            }
        }
        public string Error { get; }
        #endregion
    }
}
