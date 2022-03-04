using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.BLL.Models
{
    public class WorkStationVM : ModelBase, IDataErrorInfo
    {
        #region Properties
        public int WorkStationId { get; set; }

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

        private int divisionId;
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 100)]
        public int DivisionId
        {
            get => divisionId;
            set
            {
                divisionId = value;
                OnPropertyChanged();
            }
        }
        public DivisionVM Division { get; set; }

        public string NumberAndName => GetNumberAndName();
        #endregion

        #region Methods
        public string GetNumberAndName()
        {
            string[] sourceData = new[] { Number.ToString(), Name };
            string separator = " ";
            return string.Join(separator, sourceData);
        }
        #endregion

        #region Ctors
        public WorkStationVM() { }

        public WorkStationVM(WorkStationVM other) : this()
        {
            WorkStationId = other.WorkStationId;
            Number = other.Number;
            Name = other.Name;
            DivisionId = other.DivisionId;
            Division = other.Division;
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
                    case nameof(WorkStationId):
                        errors = GetErrorsFromAnnotations(nameof(WorkStationId), WorkStationId);
                        break;
                    case nameof(Number):
                        errors = GetErrorsFromAnnotations(nameof(Number), Number);
                        break;
                    case nameof(Name):
                        errors = GetErrorsFromAnnotations(nameof(Name), Name);
                        break;
                    case nameof(DivisionId):
                        errors = GetErrorsFromAnnotations(nameof(DivisionId), DivisionId);
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
