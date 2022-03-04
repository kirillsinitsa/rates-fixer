using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.DAL.Entities
{
    public class Division
    {
        public int DivisionId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Номер участка.")]
        [Range(1, 100)]
        public int Number { get; set; }
        [Required(ErrorMessage = "Обязательное поле Название участка.")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Обязательное поле Номер цеха.")]
        [Range(1, 100)]
        public int FactoryFloorId { get; set; }
    }
}
