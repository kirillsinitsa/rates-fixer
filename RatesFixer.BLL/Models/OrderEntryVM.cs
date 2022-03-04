using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.BLL.Models
{
    public class OrderEntryVM : ModelBase, IDataErrorInfo, IEquatable<OrderEntryVM>
    {
        #region Fields
        private int _partOperationId;
        private int _employeeId;
        private int _usefulProducts;
        private int _payedDefectProducts;
        private PaymentVM _usefulProductsPayment;
        private PaymentVM _defectProductsPayment;
        #endregion

        #region Properties
        public int OrderEntryId { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public int PartOperationId
        {
            get => _partOperationId;
            set
            {
                if (_partOperationId != value)
                {
                    _partOperationId = value;
                    OnPropertyChanged();
                }
            }
        }
        public PartOperationVM PartOperation { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 100000)]
        public int EmployeeId
        {
            get => _employeeId;
            set
            {
                if (_employeeId != value)
                {
                    _employeeId = value;
                    OnPropertyChanged();
                }
            }
        }
        public EmployeeVM Employee { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public int UsefulProducts
        {
            get => _usefulProducts;
            set
            {
                if (_usefulProducts != value)
                {
                    _usefulProducts = value;
                    OnPropertyChanged();
                }
            }
        }
        [Required(ErrorMessage = "Обязательное поле")]
        public int PayedDefectProducts
        {
            get => _payedDefectProducts;
            set
            {
                if (_payedDefectProducts != value)
                {
                    _payedDefectProducts = value;
                    OnPropertyChanged();
                }
            }
        }
        [Required(ErrorMessage = "Обязательное поле")]
        public PaymentVM UsefulProductsPayment
        {
            get => _usefulProductsPayment;
            set
            {
                if (_usefulProductsPayment != value)
                {
                    _usefulProductsPayment = value;
                    OnPropertyChanged();
                }
            }
        }
        [Required(ErrorMessage = "Обязательное поле")]
        public PaymentVM DefectProductsPayment
        {
            get => _defectProductsPayment;
            set
            {
                if (_defectProductsPayment != value)
                {
                    _defectProductsPayment = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Ctors
        public OrderEntryVM()
        {
            UsefulProductsPayment = new PaymentVM();
            DefectProductsPayment = new PaymentVM();
        }

        public OrderEntryVM(OrderEntryVM other)
        {
            OrderEntryId = other.OrderEntryId;
            PartOperationId = other.PartOperationId;
            EmployeeId = other.EmployeeId;
            UsefulProducts = other.UsefulProducts;
            PayedDefectProducts = other.PayedDefectProducts;
            UsefulProductsPayment = new PaymentVM(other.UsefulProductsPayment);
            DefectProductsPayment = new PaymentVM(other.DefectProductsPayment);
            PartOperation = new PartOperationVM(other.PartOperation);
            Employee = new EmployeeVM(other.Employee);
        }
        #endregion

        #region Methods
        public void Copy(OrderEntryVM other)
        {
            if (PartOperationId != other.PartOperationId)
            {
                PartOperationId = other.PartOperationId;
                PartOperation = other.PartOperation;
            }
            if (EmployeeId != other.EmployeeId)
            {
                EmployeeId = other.EmployeeId;
                Employee = other.Employee;
            }
            if (UsefulProducts != other.UsefulProducts) UsefulProducts = other.UsefulProducts;
            if (PayedDefectProducts != other.PayedDefectProducts) PayedDefectProducts = other.PayedDefectProducts;
            if (UsefulProductsPayment != other.UsefulProductsPayment) UsefulProductsPayment = new PaymentVM(other.UsefulProductsPayment);
            if (DefectProductsPayment != other.DefectProductsPayment) DefectProductsPayment = new PaymentVM(other.DefectProductsPayment);
        }
        public void Clear()
        {
            PartOperationId = 0;
            PartOperation = null;
            EmployeeId = 0;
            Employee = null;
            UsefulProducts = 0;
            PayedDefectProducts = 0;
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
                    case nameof(OrderEntryId):
                        errors = GetErrorsFromAnnotations(nameof(OrderEntryId), OrderEntryId);
                        break;
                    case nameof(OrderId):
                        errors = GetErrorsFromAnnotations(nameof(OrderId), OrderId);
                        break;
                    case nameof(PartOperationId):
                        errors = GetErrorsFromAnnotations(nameof(PartOperationId), PartOperationId);
                        break;
                    case nameof(EmployeeId):
                        errors = GetErrorsFromAnnotations(nameof(EmployeeId), EmployeeId);
                        break;
                    case nameof(UsefulProducts):
                        errors = GetErrorsFromAnnotations(nameof(UsefulProducts), UsefulProducts);
                        break;
                    case nameof(PayedDefectProducts):
                        errors = GetErrorsFromAnnotations(nameof(PayedDefectProducts), PayedDefectProducts);
                        break;
                    case nameof(UsefulProductsPayment):
                        errors = GetErrorsFromAnnotations(nameof(UsefulProductsPayment), UsefulProductsPayment);
                        break;
                    case nameof(DefectProductsPayment):
                        errors = GetErrorsFromAnnotations(nameof(DefectProductsPayment), DefectProductsPayment);
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
        public bool Equals(OrderEntryVM other)
        {
            return EmployeeId == other.EmployeeId &&
                   PartOperationId == other.PartOperationId &&
                   UsefulProducts == other.UsefulProducts &&
                   PayedDefectProducts == other.PayedDefectProducts &&
                   UsefulProductsPayment.Sum == other.UsefulProductsPayment.Sum &&
                   DefectProductsPayment.Sum == other.DefectProductsPayment.Sum;
        }

        public override bool Equals(object obj)
        {
            if (obj is OrderEntryVM)
                return Equals((OrderEntryVM)obj);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public static bool operator ==(OrderEntryVM orderEntry1, OrderEntryVM orderEntry2)
        {
            if ((object)orderEntry1 == null || (object)orderEntry2 == null)
                return Equals(orderEntry1, orderEntry2);
            else
                return orderEntry1.Equals(orderEntry2);
        }

        public static bool operator !=(OrderEntryVM orderEntry1, OrderEntryVM orderEntry2)
        {
            if ((object)orderEntry1 == null || (object)orderEntry2 == null)
                return !Equals(orderEntry1, orderEntry2);
            else
                return !orderEntry1.Equals(orderEntry2);
        }
        #endregion
    }
}
