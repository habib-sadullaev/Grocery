namespace Grocery.PointOfSale.Implementations;

using Grocery.PointOfSale.Interfaces;
using Grocery.PointOfSale.Models;

internal class CartRepository : ICartRepository
{
    private Context context;
    private static int counter = 0;
    private static string CartKey(int cartId) => $"cart_{cartId}";

    public CartRepository(Context context)
    {
        this.context = context;
    }

    public Cart Create()
    {
        var cart = new Cart(++counter, new List<CartItem>());
        context.Store.Add(CartKey(counter), cart);

        return cart;
    }
}
