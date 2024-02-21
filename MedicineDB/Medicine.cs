using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.MedicineDB
{
    public class Medicine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(255, ErrorMessage = "Name cannot be longer than 255 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Form is required")]
        [MaxLength(100, ErrorMessage = "Form cannot be longer than 100 characters")]
        public string? Form { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Dosage must be a positive number")]
        public int Dosage { get; set; }

        [ForeignKey("Manufacturers")]
        public int Manufacturers_ID { get; set; }

        public string? Description {  get; set; }

        public Manufacturer Manufacturers { get; set; }

        [InverseProperty("Medicines")]
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
