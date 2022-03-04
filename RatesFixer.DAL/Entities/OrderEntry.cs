using System.ComponentModel.DataAnnotations;

namespace RatesFixer.DAL.Entities
{
    public class OrderEntry
    {
        public int OrderEntryId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Id наряда.")]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Id операции детали.")]
        public int PartOperationId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Таб. номер рабочего.")]
        [Range(1, 100000)]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Количество годных деталей.")]
        public int UsefulProducts { get; set; }
        [Required(ErrorMessage = "Обязательное поле Количество оплачиваемых бракованных деталей.")]
        public int PayedDefectProducts { get; set; }
        [Required(ErrorMessage = "Обязательное поле Оплата за годные.")]
        public Payment UsefulProductsPayment { get; set; }
        [Required(ErrorMessage = "Обязательное поле Оплата за брак.")]
        public Payment DefectProductsPayment { get; set; }
        public Order Order { get; set; }
    }
}
