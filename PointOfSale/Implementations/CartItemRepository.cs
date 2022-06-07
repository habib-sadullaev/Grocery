namespace Grocery.PointOfSale.Implementations;

using Grocery.PointOfSale.Interfaces;
using Grocery.PointOfSale.Models;

internal class CartItemRepository : ICartItemRepository
{
    private Context context;

    public CartItemRepository(Context context)
    {
        this.context = context;
    }

    private static string CartItemKey(int cartId, string productCode) => $"cartItem_{cartId}_{productCode}";
    private static string CartKey(int cartId) => $"cart_{cartId}";

    public void AddCartItems(Cart cart, Product product)
    {
        var cartItemkey = CartItemKey(cart.Id, product.Code);
        if (context.Store.TryGetValue(cartItemkey, out var obj) && obj is CartItem cartItem)
            cartItem.Amount += 1;
        else
        {
            cartItem = new CartItem(cart, product, 1);
            context.Store[cartItemkey] = cartItem;
            cart.Items.Add(cartItem);
        }
    }

    public IReadOnlyCollection<CartItem> GetCartItems(int cartId)
    {
        if (context.Store.TryGetValue(CartKey(cartId), out var obj) && obj is Cart cart)
            return cart.Items.ToList();

        return Array.Empty<CartItem>();
    }
}
