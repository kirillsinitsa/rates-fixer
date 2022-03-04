using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RatesFixer.DAL.Entities
{
    public class FactoryFloor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Range(1, 100)]
        public int FactoryFloorId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Название цеха.")]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
