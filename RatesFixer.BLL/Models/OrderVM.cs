using System.Collections.ObjectModel;
using RatesFixer.DAL.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.BLL.Models
{
    public class OrderVM : ModelBase, IDataErrorInfo, IEquatable<OrderVM>
    {
        #region Fields and properties
        public int OrderId { get; set; }

        private string _number;
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(20, ErrorMessage = "Длина не должна превышать 20 символов.")]
        public string Number
        { 
            get => _number;
            set
            {
                if (_number != value)
                {
                    _number = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _date;
        [Required(ErrorMessage = "Обязательное поле")]
        public DateTime Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _year;
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1900, 2200)]
        public int Year
        {
            get => _year;
            set
            {
                if (_year != value)
                {
                    _year = value;
                    OnPropertyChanged();
                }
            }
        }

        private Month _month;
        [Required(ErrorMessage = "Обязательное поле")]
        [EnumDataType(typeof(Month))]
        public Month Month
        {
            get => _month;
            set
            {
                if (_month != value)
                {
                    _month = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _factoryFloorId;
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 100)]
        public int FactoryFloorId
        {
            get => _factoryFloorId;
            set
            {
                if (_factoryFloorId != value)
                {
                    _factoryFloorId = value;
                    OnPropertyChanged();
                }
            }
        }

        private FactoryFloorVM factoryFloor;
        public FactoryFloorVM FactoryFloor
        {
            get => factoryFloor;
            set
            {
                factoryFloor = value;
                OnPropertyChanged();
            }
        }

        private int _divisionId;
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 100)]
        public int DivisionId
        {
            get => _divisionId;
            set
            {
                if (_divisionId != value)
                {
                    _divisionId = value;
                    OnPropertyChanged();
                }
            }
        }

        private DivisionVM division;
        public DivisionVM Division
        {
            get => division;
            set
            {
                division = value;
                OnPropertyChanged();
            }
        }

        private int _partId;
        [Required(ErrorMessage = "Обязательное поле")]
        public int PartId
        {
            get => _partId;
            set
            {
                if (_partId != value)
                {
                    _partId = value;
                    OnPropertyChanged();
                }
            }
        }

        private PartVM part;
        public PartVM Part
        {
            get => part;
            set
            {
                part = value;
                OnPropertyChanged();
            }
        }

        private string _usefulProductsPaymentType;
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(10, ErrorMessage = "Длина не должна превышать 10 символов.")]
        public string UsefulProductsPaymentType
        {
            get => _usefulProductsPaymentType;
            set
            {
                if (_usefulProductsPaymentType != value)
                {
                    _usefulProductsPaymentType = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _defectProductsPaymentType;
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(10, ErrorMessage = "Длина не должна превышать 10 символов.")]
        public string DefectProductsPaymentType
        {
            get => _defectProductsPaymentType;
            set
            {
                if (_defectProductsPaymentType != value)
                {
                    _defectProductsPaymentType = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<OrderEntryVM> orderEntries;
        public ObservableCollection<OrderEntryVM> OrderEntries
        {
            get => orderEntries;
            set
            {
                orderEntries = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Ctros
        public OrderVM()
        {
            OrderEntries = new ObservableCollection<OrderEntryVM>();
        }

        public OrderVM(OrderVM other) : this()
        {
            OrderId = other.OrderId;
            Number = other.Number;
            Month = other.Month;
            Year = other.Year;
            Date = other.Date;
            DivisionId = other.DivisionId;
            FactoryFloorId = other.FactoryFloorId;
            PartId = other.PartId;
            UsefulProductsPaymentType = other.UsefulProductsPaymentType;
            DefectProductsPaymentType = other.DefectProductsPaymentType;
            Division = new DivisionVM(other.Division);
            FactoryFloor = new FactoryFloorVM(other.FactoryFloor);
            Part = new PartVM(other.Part);
            foreach (OrderEntryVM orderEntry in other.OrderEntries)
            {
                OrderEntries.Add(new OrderEntryVM(orderEntry));
            }
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
                    case nameof(OrderId):
                        errors = GetErrorsFromAnnotations(nameof(OrderId), OrderId);
                        break;
                    case nameof(Number):
                        errors = GetErrorsFromAnnotations(nameof(Number), Number);
                        break;
                    case nameof(Date):
                        errors = GetErrorsFromAnnotations(nameof(Date), Date);
                        break;
                    case nameof(Month):
                        errors = GetErrorsFromAnnotations(nameof(Month), Month);
                        break;
                    case nameof(Year):
                        errors = GetErrorsFromAnnotations(nameof(Year), Year);
                        break;
                    case nameof(FactoryFloorId):
                        errors = GetErrorsFromAnnotations(nameof(FactoryFloorId), FactoryFloorId);
                        break;
                    case nameof(DivisionId):
                        errors = GetErrorsFromAnnotations(nameof(DivisionId), DivisionId);
                        break;
                    case nameof(PartId):
                        errors = GetErrorsFromAnnotations(nameof(PartId), PartId);
                        break;
                    case nameof(UsefulProductsPaymentType):
                        errors = GetErrorsFromAnnotations(nameof(UsefulProductsPaymentType), UsefulProductsPaymentType);
                        break;
                    case nameof(DefectProductsPaymentType):
                        errors = GetErrorsFromAnnotations(nameof(DefectProductsPaymentType), DefectProductsPaymentType);
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

        #region IEquatable members
        public bool Equals(OrderVM other)
        {
            return Number == other.Number &&
                   Month == other.Month &&
                   Year == other.Year &&
                   Date == other.Date &&
                   DivisionId == other.DivisionId &&
                   FactoryFloorId == other.FactoryFloorId &&
                   PartId == other.PartId &&
                   UsefulProductsPaymentType == other.UsefulProductsPaymentType &&
                   DefectProductsPaymentType == other.DefectProductsPaymentType; 
        }

        public override bool Equals(object obj)
        {
            if (obj is OrderVM)
                return Equals((OrderVM)obj);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public static bool operator ==(OrderVM order1, OrderVM order2)
        {
            if ((object)order1 == null || (object)order2 == null)
                return Equals(order1, order2);
            else
                return order1.Equals(order2);
        }

        public static bool operator !=(OrderVM order1, OrderVM order2)
        {
            if ((object)order1 == null || (object)order2 == null)
                return !Equals(order1, order2);
            else
                return !order1.Equals(order2);
        }
        #endregion
    }
}
