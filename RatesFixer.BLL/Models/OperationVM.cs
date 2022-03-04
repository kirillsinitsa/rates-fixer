using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.BLL.Models
{
    public class OperationVM : ModelBase, IDataErrorInfo
    {
        #region Properties
        public int OperationId { get; set; }
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

        private int jobClass;
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 6)]
        public int JobClass
        {
            get => jobClass;
            set
            {
                jobClass = value;
                OnPropertyChanged();
            }
        }

        private string operationCode;
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(10, ErrorMessage = "Длина не должна превышать 10 символов.")]
        public string OperationCode
        {
            get => operationCode;
            set
            {
                operationCode = value;
                OnPropertyChanged();
            }
        }

        private int tariffPayGroupId;
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 3)]
        public int TariffPayGroupId
        {
            get => tariffPayGroupId;
            set
            {
                tariffPayGroupId = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Ctors
        public OperationVM() { }

        public OperationVM(OperationVM other)
        {
            OperationId = other.OperationId;
            Name = other.Name;
            JobClass = other.JobClass;
            OperationCode = other.OperationCode;
            TariffPayGroupId = other.TariffPayGroupId;
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
                    case nameof(OperationId):
                        errors = GetErrorsFromAnnotations(nameof(OperationId), OperationId);
                        break;
                    case nameof(Name):
                        errors = GetErrorsFromAnnotations(nameof(Name), Name);
                        break;
                    case nameof(JobClass):
                        errors = GetErrorsFromAnnotations(nameof(JobClass), JobClass);
                        break;
                    case nameof(OperationCode):
                        errors = GetErrorsFromAnnotations(nameof(OperationCode), OperationCode);
                        break;
                    case nameof(TariffPayGroupId):
                        errors = GetErrorsFromAnnotations(nameof(TariffPayGroupId), TariffPayGroupId);
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
