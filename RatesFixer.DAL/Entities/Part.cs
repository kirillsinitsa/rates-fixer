using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.DAL.Entities
{
    public class Part
    {
        public int PartId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Номер детали.")]
        [MaxLength(50)]
        public string Number { get; set; }
        [Required(ErrorMessage = "Обязательное поле Наименование детали.")]
        [MaxLength(50)]
        public string Name { get; set; }
        public ObservableCollection<PartOperation> PartOperations { get; set; }
    }
}
