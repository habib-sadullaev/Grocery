namespace Grocery.PointOfSale.Implementations;

using Grocery.PointOfSale.Interfaces;
using Grocery.PointOfSale.Models;
using static Utils;

internal class CartItemRepository : ICartItemRepository
{
    private Context context;

    public CartItemRepository(Context context)
    {
        this.context = context;
    }

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
        if (!context.Store.TryGetValue(CartKey(cartId), out var obj) || obj is not Cart cart)
            throw new InvalidOperationException($"Cart with id = '{cartId}' does not exist");

        return cart.Items.ToList();
    }
}
