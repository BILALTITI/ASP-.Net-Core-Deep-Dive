using M01.ModelAndInMemoryStoreSetup.Model;

namespace M01.ModelAndInMemoryStoreSetup.Store
{
    public class ProductStore
    {
        private readonly List<Product> _products = [


                new Product { Id = Guid.NewGuid(), Name = "Laptop", Price = 999.99m },
                new Product { Id = Guid.NewGuid(), Name = "Smartphone", Price = 499.99m },
                new Product { Id = Guid.NewGuid(), Name = "Tablet", Price = 299.99m }


            ];
        public List<Product> Products { get; set; } = new();

        public IEnumerable<Product> GetAll() => _products;

        public Product? GetById(Guid id) => _products.FirstOrDefault(p => p.Id == id);

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public bool Update(Product updatedProduct)
        {
            var existing = _products.FirstOrDefault(p => p.Id == updatedProduct.Id);

            if (existing is null)
                return false;

            existing.Name = updatedProduct.Name;
            existing.Price = updatedProduct.Price;

            return true;
        }

        public bool Delete(Product product)
        {
            var existing = _products.FirstOrDefault(p => p.Id == product.Id);

            return existing != null && _products.Remove(product);
        }
    }
}
