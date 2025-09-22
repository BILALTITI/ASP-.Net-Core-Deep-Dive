using M01.ModelAndInMemoryStoreSetup.Model;
using M01.ModelAndInMemoryStoreSetup.Store;
using Microsoft.AspNetCore.Mvc;

namespace _02MvcCrud.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductStore store;

        // Constructor injection
        public ProductsController(ProductStore store)
        {
            this.store = store;
        }

        // GET: Products
        public IActionResult Index()
        {
            var products = store.GetAll(); // must return IEnumerable<Product>
            return View(products);
        }

        // GET: Products/Details/{id}
        public IActionResult Details(Guid id)
        {
            var product = store.GetById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create() => View();

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid) return View(product);

            product.Id = Guid.NewGuid();
            store.Add(product);

            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/{id}
        public IActionResult Edit(Guid id)
        {
            var product = store.GetById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: Products/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Product product)
        {
            if (!ModelState.IsValid) return View(product);

            var existing = store.GetById(id);
            if (existing == null) return NotFound();

            existing.Name = product.Name;
            existing.Price = product.Price;

            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Delete/{id}
        public IActionResult Delete(Guid id)
        {
            var product = store.GetById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: Products/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            //store.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
