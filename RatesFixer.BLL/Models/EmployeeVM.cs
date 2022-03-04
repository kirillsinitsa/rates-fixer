using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.BLL.Models
{
    public class EmployeeVM : ModelBase, IDataErrorInfo
    {
        #region Properties
        private int employeeId;
        [Key]
        [Required(ErrorMessage ="Обязательное поле")]
        [Range(1, 100000)]
        public int EmployeeId
        {
            get => employeeId;
            set
            {
                employeeId = value;
                OnPropertyChanged();
            }
        }

        private string lastName;
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(50, ErrorMessage = "Длина не должна превышать 50 символов.")]
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged();
            }
        }

        private string firstName;
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(50, ErrorMessage = "Длина не должна превышать 50 символов.")]
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged();
            }
        }

        private string patronymic;
        [MaxLength(50, ErrorMessage = "Длина не должна превышать 50 символов.")]
        public string Patronymic
        {
            get => patronymic;
            set
            {
                patronymic = value;
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

        private int occupationCode;
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 100000)]
        public int OccupationCode
        {
            get => occupationCode;
            set
            {
                occupationCode = value;
                OnPropertyChanged();
            }
        }

        private int workStationId;
        [Range(1, 100)]
        public int WorkStationId
        {
            get => workStationId;
            set
            {
                workStationId = value;
                OnPropertyChanged();
            }
        }
        public WorkStationVM WorkStation { get; set; }
        public string FullName => GetFullName();
        public string LastNameWithInitials => GetLastNameWithInitials();
        #endregion

        #region Methods
        private string GetFullName()
        {
            return string.Join(" ", new[] { LastName, FirstName, Patronymic });
        }

        private string GetLastNameWithInitials()
        {
            string initials = string.Join(".", new[] {FirstName.Substring(0,1), Patronymic.Substring(0, 1), string.Empty });

            return string.Join(" ", new[] { LastName, initials });
        }
        #endregion

        #region Ctors
        public EmployeeVM() { }
        public EmployeeVM(EmployeeVM other)
        {
            if (other == null) return;
            EmployeeId = other.EmployeeId;
            LastName = other.LastName;
            FirstName = other.FirstName;
            Patronymic = other.Patronymic;
            OccupationCode = other.OccupationCode;
            JobClass = other.JobClass;
            WorkStationId = other.WorkStationId;
            WorkStation = other.WorkStation;
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
                    case nameof(EmployeeId):
                        errors = GetErrorsFromAnnotations(nameof(EmployeeId), EmployeeId);
                        break;
                    case nameof(LastName):
                        errors = GetErrorsFromAnnotations(nameof(LastName), LastName);
                        break;
                    case nameof(FirstName):
                        errors = GetErrorsFromAnnotations(nameof(FirstName), FirstName);
                        break;
                    case nameof(Patronymic):
                        errors = GetErrorsFromAnnotations(nameof(Patronymic), Patronymic);
                        break;
                    case nameof(JobClass):
                        errors = GetErrorsFromAnnotations(nameof(JobClass), JobClass);
                        break;
                    case nameof(OccupationCode):
                        errors = GetErrorsFromAnnotations(nameof(OccupationCode), OccupationCode);
                        break;
                    case nameof(WorkStationId):
                        errors = GetErrorsFromAnnotations(nameof(WorkStationId), WorkStationId);
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
