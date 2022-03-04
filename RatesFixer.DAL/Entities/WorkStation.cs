using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.DAL.Entities
{
    public class WorkStation
    {       
        public int WorkStationId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Номер рабочего места.")]
        [Range(1, 100)]
        public int Number { get; set; }
        [Required(ErrorMessage = "Обязательное поле Наименование рабочего места.")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Обязательное поле Id участка.")]
        [Range(1, 100)]
        public int DivisionId { get; set; }
    }
}
