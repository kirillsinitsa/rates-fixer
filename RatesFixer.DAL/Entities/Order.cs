using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using RatesFixer.DAL.Enums;

namespace RatesFixer.DAL.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Номер наряда.")]
        [MaxLength(20)]
        public string Number { get; set; }
        [Required(ErrorMessage = "Обязательное поле Дата.")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Обязательное поле Год.")]
        [Range(1900, 2200)]
        public int Year { get; set; }
        [Required(ErrorMessage = "Обязательное поле Месяц.")]
        [EnumDataType(typeof(Month))]
        public Month Month { get; set; }
        [Required(ErrorMessage = "Обязательное поле Номер цеха.")]
        [Range(1, 100)]
        public int FactoryFloorId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Id участка.")]
        [Range(1, 100)]
        public int DivisionId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Id детали.")]
        public int PartId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Код вида оплаты годных деталей.")]
        [MaxLength(10)]
        public string UsefulProductsPaymentType { get; set; }
        [Required(ErrorMessage = "Обязательное поле Код вида оплаты оплачиваемого брака.")]
        [MaxLength(10)]
        public string DefectProductsPaymentType { get; set; }
        public ObservableCollection<OrderEntry> OrderEntries { get; set; }
    }
}
