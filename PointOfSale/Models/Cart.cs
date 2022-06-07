namespace Grocery.PointOfSale.Models;

public record Cart(int Id, ICollection<CartItem> Items);
