namespace Grocery.PointOfSale;

public interface IProductRepository
{
    void Create(string productCode, decimal productPrice, int? discountAmount = null, decimal? discountPrice = null);
    Product Get(string productCode);
}
