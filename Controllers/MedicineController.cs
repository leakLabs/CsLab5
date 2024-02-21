using Lab4.MedicineDB;
using lab5.MedicineDB;
using lab5.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace lab5.Controllers
{
    public class MedicineController : Controller
    {
        private readonly DBControl<Medicine> dbControl;
        private readonly DBControl<Ingredient> dbControlIng;
        private readonly DBControl<Manufacturer> dbControlManuf;
        private readonly ApplicationDbContext context;

        public MedicineController(ApplicationDbContext _context)
        {
            dbControl = new DBControl<Medicine>(_context);
            dbControlIng = new DBControl<Ingredient>(_context);
            dbControlManuf = new DBControl<Manufacturer>(_context);
            context = _context;
        }

        [HttpGet]
        [Route("Medicine/Index")]
        public IActionResult Index()
        {
            //List<Medicine> medicines = dbControl.GetAllWithInclude("Manufacturers", "Ingredients");
            //return View(medicines);
            indexViewModel model = new indexViewModel { medicine = dbControl.GetAllWithInclude("Ingredients"), Manufacturers = dbControlManuf.GetAll() };
            return View(model);
        }

        [HttpGet]
        [Route("Medicine/Index/{id}")]
        public IActionResult Index(int id)
        {
            Medicine? medicine = dbControl.GetAllWithInclude("Ingredients").FirstOrDefault(medicine => id == medicine.ID);
            if (medicine != null)
            {
                createViewModel viewModel = new createViewModel
                {
                    medicine = medicine,
                    Ingredients = dbControlIng.GetAll(),
                    Manufacturers = dbControlManuf.GetAll(),
                    IDs = medicine.Ingredients.Select(i => i.ID).ToList()
                };
                return View("MedicineView", viewModel);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Create()
        {
            createViewModel model = new createViewModel { Ingredients = dbControlIng.GetAll(), Manufacturers = dbControlManuf.GetAll() };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(createViewModel model)
        {
            ModelState.Remove("medicine.Manufacturers");
            if (!ModelState.IsValid)
            {
                model.Ingredients = new DBControl<Ingredient>(context).GetAll();
                model.Manufacturers = new DBControl<Manufacturer>(context).GetAll();
                return View(model);
            }

            Medicine medicine = model.medicine;
            List<Ingredient> ingredients = dbControlIng.GetAll().ToList();
            medicine.Ingredients = ingredients.Where(ing => model.IDs.Contains(ing.ID)).ToList();
            dbControl.Insert(medicine);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                Medicine? medicine = dbControl.GetAllWithInclude("Ingredients").FirstOrDefault(medicine => id.Value == medicine.ID);
                if (medicine != null)
                {
                    createViewModel viewModel = new createViewModel
                    {
                        medicine = medicine,
                        Ingredients = dbControlIng.GetAll(),
                        Manufacturers = dbControlManuf.GetAll(),
                        IDs = medicine.Ingredients.Select(i => i.ID).ToList()
                    };
                    return View(viewModel);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(createViewModel model)
        {
            ModelState.Remove("medicine.Manufacturers");
            if (!ModelState.IsValid)
            {
                model.Ingredients = dbControlIng.GetAll();
                model.Manufacturers = dbControlManuf.GetAll();
                return View(model);
            }
            Medicine medicine = model.medicine;
            List<Ingredient> ingredients = dbControlIng.GetAll().ToList();
            medicine.Ingredients = ingredients.Where(ing => model.IDs.Contains(ing.ID)).ToList();
            //medicine.Ingredients = null;
            dbControl.Delete(medicine.ID);
            medicine.ID = 0;
            dbControl.Insert(medicine);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                dbControl.Delete(id.Value);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public IActionResult Redirect()
        {
            return RedirectToAction("Index");
        }
    }
}
