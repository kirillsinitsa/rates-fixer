using System.ComponentModel.DataAnnotations;

namespace RatesFixer.DAL.Entities
{
    public class PartOperation
    {
        public int PartOperationId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Номер операции по тех. процессу.")]
        public string Number { get; set; }
        [Required(ErrorMessage = "Обязательное поле Id операции.")]
        public int OperationId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Норма времени.")]
        public double RateTime { get; set; }
        [Required(ErrorMessage = "Обязательное поле Процент выполнения.")]
        public double Percentage { get; set; }
        public int WageRateId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Id участка.")]
        public int DivisionId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Id цеха.")]
        public int FactoryFloorId { get; set; }
        [Required(ErrorMessage = "Обязательное поле  Id детали.")]
        public int PartId { get; set; }
        public Part Part { get; set; }
    }
}
