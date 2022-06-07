namespace Grocery.PointOfSale.Implementations;

using Grocery.PointOfSale.Interfaces;

public class Context : IUnitOfWork
{
    internal protected Dictionary<string, object> Store { get; } = new();
    
    public Context()
    {
        Products = new ProductRepository(this);
        CartItems = new CartItemRepository(this);
        Carts = new CartRepository(this);
    }

    public IProductRepository Products { get; }

    public ICartItemRepository CartItems { get; }

    public ICartRepository Carts { get; }
}
