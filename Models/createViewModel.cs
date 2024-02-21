using Lab4.MedicineDB;
using System.Web.Mvc;

namespace lab5.Models
{
    public class createViewModel
    {
        public IEnumerable<int> IDs { get; set; } = new List<int>();
        public IEnumerable<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public IEnumerable<Manufacturer> Manufacturers { get; set; } = new List<Manufacturer>();
        public Medicine medicine { get; set; }
    }
}
