using System.Collections.ObjectModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.BLL.Models
{
    public class PartVM : ModelBase, IDataErrorInfo
    {
        #region Properties
        public int PartId { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(50)]
        public string Number { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(50, ErrorMessage = "Длина не должна превышать 50 символов.")]
        public string Name { get; set; }
        public ObservableCollection<PartOperationVM> Operations { get; set; }
        #endregion

        #region Ctors
        public PartVM() { }

        public PartVM(PartVM other)
        {
            PartId = other.PartId;
            Number = other.Number;
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
                    case nameof(PartId):
                        errors = GetErrorsFromAnnotations(nameof(PartId), PartId);
                        break;
                    case nameof(Number):
                        errors = GetErrorsFromAnnotations(nameof(Number), Number);
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
