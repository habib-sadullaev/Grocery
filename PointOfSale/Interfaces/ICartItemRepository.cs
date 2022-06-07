namespace Grocery.PointOfSale.Interfaces;

using Grocery.PointOfSale.Models;

public interface ICartItemRepository
{
    void AddCartItems(Cart cart, Product product);
    IReadOnlyCollection<CartItem> GetCartItems(int cartId);
}
