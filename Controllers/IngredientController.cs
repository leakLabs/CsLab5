using Lab4.MedicineDB;
using lab5.MedicineDB;
using Microsoft.AspNetCore.Mvc;

namespace lab5.Controllers
{
    public class IngredientController:Controller
    {
        private readonly DBControl<Ingredient> dbControl;

        public IngredientController(ApplicationDbContext context)
        {
            //_context = context;
            dbControl = new DBControl<Ingredient>(context);
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Ingredient> ingredients = dbControl.GetAllWithInclude("Medicines");
            return View(ingredients);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Ingredient ingredient)
        {
            if (!ModelState.IsValid)
                return View();
            dbControl.Insert(ingredient);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                Ingredient? ingredient = dbControl.GetById(id.Value);
                if (ingredient != null) return View(ingredient);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Ingredient ingredient)
        {
            if (!ModelState.IsValid)
                return View(ingredient);
            dbControl.Update(ingredient);
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
    }
}
