namespace Grocery.PointOfSale;

public interface ICartItemRepository
{
    void AddCartItems(Cart cart, Product product);
    IReadOnlyCollection<CartItem> GetCartItems(int cartId);
}
