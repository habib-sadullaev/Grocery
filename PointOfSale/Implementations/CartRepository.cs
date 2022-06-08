namespace Grocery.PointOfSale.Implementations;

using Grocery.PointOfSale.Interfaces;
using Grocery.PointOfSale.Models;
using static Utils;

internal class CartRepository : ICartRepository
{
    private readonly Context context;
    private static int counter = 0;

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
