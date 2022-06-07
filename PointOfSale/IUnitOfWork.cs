namespace Grocery.PointOfSale;

public interface IUnitOfWork
{
    IProductRepository Products { get; }
    ICartItemRepository CartItems { get; }
    ICartRepository Carts { get; }
}
