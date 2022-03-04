using System.ComponentModel.DataAnnotations;

namespace RatesFixer.DAL.Entities
{
    public class Operation
    {
        public int OperationId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Название операции.")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Обязательное поле Разряд.")]
        [Range(1, 6)]
        public int JobClass { get; set; }
        [Required(ErrorMessage = "Обязательное поле Код операции.")]
        [MaxLength(10)]
        public string OperationCode { get; set; }
        [Required(ErrorMessage = "Обязательное поле Номер группы тарифных ставок.")]
        [Range(1, 3)]
        public int TariffPayGroupId { get; set; }
    }
}
