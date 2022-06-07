namespace Grocery.PointOfSale.Models;

public record CartItem(Cart Cart, Product Product, int Amount)
{
    public int Amount { get; set; } = Amount;
}
