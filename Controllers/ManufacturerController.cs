using Lab4.MedicineDB;
using lab5.MedicineDB;
using Microsoft.AspNetCore.Mvc;

namespace lab5.Controllers
{
    public class ManufacturerController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly DBControl<Manufacturer> dbControl;

        public ManufacturerController(ApplicationDbContext context)
        {
            //_context = context;
            dbControl = new DBControl<Manufacturer>(context);
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Manufacturer> medicines = dbControl.GetAllWithInclude("Medicines");
            return View(medicines);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
                return View();
            dbControl.Insert(manufacturer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                Manufacturer? manufacturer = dbControl.GetById(id.Value);
                if (manufacturer != null) return View(manufacturer);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
                return View(manufacturer);
            dbControl.Update(manufacturer);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if(id != null)
            {
                dbControl.Delete(id.Value);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
