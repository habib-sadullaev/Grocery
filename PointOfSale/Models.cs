namespace Grocery.PointOfSale;
public record Product(string Code, ProductPrice Price, ProductDiscount? Discount);

public record ProductPrice(Product Product, decimal Price);

public record ProductDiscount(Product Product, int Amount, decimal Price);

public record CartItem(Cart cart, Product Product, int Amount);

public record Cart(int Id, ICollection<CartItem> Items);
