namespace Grocery.PointOfSale.Interfaces;

using Grocery.PointOfSale.Models;

public interface IProductRepository
{
    void Create(string productCode, decimal productPrice, int? discountAmount = null, decimal? discountPrice = null);
    Product Get(string productCode);
}
