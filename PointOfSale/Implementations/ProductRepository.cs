namespace Grocery.PointOfSale.Implementations;

using Grocery.PointOfSale.Interfaces;
using Grocery.PointOfSale.Models;

internal class ProductRepository : IProductRepository
{
    private readonly Context context;

    public ProductRepository(Context context)
    {
        this.context = context;
    }

    public void Create(string productCode, decimal productPrice, int? discountAmount = null, decimal? discountPrice = null)
    {
        var key = ProductKey(productCode);
        if (context.Store.TryGetValue(key, out _))
            new InvalidOperationException($"a product with code '{productCode} already exists'");

        var product = new Product(productCode);
        var price = new ProductPrice(product, productPrice);
        product = product with { Price = price };

        if (discountAmount.HasValue && discountPrice.HasValue)
        {
            var discount = new ProductDiscount(product, discountAmount.Value, discountPrice.Value);
            product = product with { Discount = discount };
        }

        context.Store[key] = product;
    }

    private static string ProductKey(string productCode) => $"product_{productCode}";

    public Product Get(string productCode)
    {
        if (context.Store.TryGetValue(ProductKey(productCode), out var val) && val is Product product)
            return product;
        throw new InvalidOperationException($"a product with code '{productCode}' does not exist");
    }
}
