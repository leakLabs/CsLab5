using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.MedicineDB
{
    public class Manufacturer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(255, ErrorMessage = "Name cannot be longer than 255 characters")]
        public string? Name { get; set; }

        public string? Country { get; set; }

        public string? Contact_information { get; set; }

        public ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
    }
}
