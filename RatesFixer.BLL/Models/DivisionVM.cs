using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.BLL.Models
{
    public class DivisionVM : ModelBase, IDataErrorInfo
    {
        #region Properties
        public int DivisionId { get; set; }
        private int number;
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 100)]
        public int Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged();
            }
        }

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

        private int factoryFloorId;
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 100)]
        public int FactoryFloorId
        {
            get => factoryFloorId;
            set
            {
                factoryFloorId = value;
                OnPropertyChanged();
            }
        }

        public FactoryFloorVM FactoryFloor { get; set; }
        #endregion

        #region Ctors
        public DivisionVM() { }
        public DivisionVM(DivisionVM other)
        {
            DivisionId = other.DivisionId;
            Number = other.Number;
            Name = other.Name;
            FactoryFloorId = other.FactoryFloorId;
            FactoryFloor = other.FactoryFloor;
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
                    case nameof(DivisionId):
                        errors = GetErrorsFromAnnotations(nameof(DivisionId), DivisionId);
                        break;
                    case nameof(Number):
                        errors = GetErrorsFromAnnotations(nameof(Number), Number);
                        break;
                    case nameof(Name):
                        errors = GetErrorsFromAnnotations(nameof(Name), Name);
                        break;
                    case nameof(FactoryFloorId):
                        errors = GetErrorsFromAnnotations(nameof(FactoryFloorId), FactoryFloorId);
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
