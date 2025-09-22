using M01.ModelAndInMemoryStoreSetup.Model;
using M01.ModelAndInMemoryStoreSetup.Store;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _04_Razor_Pages_CRUD.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ProductStore _store;

        public IndexModel(ProductStore store)
        {
            _store = store;
        }

        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();

        public void OnGet()
        {
            Products = _store.GetAll();
        }
    }
}
