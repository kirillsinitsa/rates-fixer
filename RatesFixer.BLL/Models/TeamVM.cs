using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.BLL.Models
{
    public class TeamVM : ModelBase, IDataErrorInfo
    {
        #region Fields and Properties
        public int TeamId { get; set; }
        private int divisionId;
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 100)]
        public int DivisionId
        {
            get => divisionId;
            set
            {
                divisionId = value;
                OnErrorsChanged();
            }
        }
        public DivisionVM Division { get; set; }

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
        private string note;
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(100, ErrorMessage = "Длина не должна превышать 100 символов.")]
        public string Note
        {
            get => note;
            set
            {
                note = value;
                OnPropertyChanged();
            }
        }

        private int foremanId;
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 100)]
        public int ForemanId
        {
            get => foremanId;
            set
            {
                foremanId = value;
                OnPropertyChanged();
            }
        }
        public EmployeeVM Foreman { get; set; }
        #endregion

        #region Ctors
        public TeamVM() { }

        public TeamVM(TeamVM other)
        {
            TeamId = other.TeamId;
            DivisionId = other.DivisionId;
            Number = other.Number;
            Note = other.Note;
            ForemanId = other.ForemanId;
            Division = other.Division;
            Foreman = other.Foreman;
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
                    case nameof(Note):
                        errors = GetErrorsFromAnnotations(nameof(Note), Note);
                        break;
                    case nameof(ForemanId):
                        errors = GetErrorsFromAnnotations(nameof(ForemanId), ForemanId);
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
