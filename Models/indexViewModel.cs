using Lab4.MedicineDB;
using System.Web.Mvc;

namespace lab5.Models
{
    public class indexViewModel
    {
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public IEnumerable<Medicine> medicine { get; set; }
    }
}
