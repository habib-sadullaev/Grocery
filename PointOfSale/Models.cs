namespace Grocery.PointOfSale;
public record Product(string Code)
{
    public ProductPrice Price { get; set; } = null!;
    public ProductDiscount? Discount { get; set; }
}

public record ProductPrice(Product Product, decimal Price);

public record ProductDiscount(Product Product, int Amount, decimal Price);

public record CartItem(Cart Cart, Product Product, int Amount)
{
    public int Amount { get; set; } = Amount;
}

public record Cart(int Id, ICollection<CartItem> Items);
