namespace Grocery.PointOfSale.Models;
public record Product(string Code)
{
    public ProductPrice Price { get; set; } = null!;
    public ProductDiscount? Discount { get; set; }
}
