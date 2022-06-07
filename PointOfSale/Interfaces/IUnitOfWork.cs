namespace Grocery.PointOfSale.Interfaces;

using Grocery.PointOfSale.Models;

public interface IUnitOfWork
{
    IProductRepository Products { get; }
    ICartItemRepository CartItems { get; }
    ICartRepository Carts { get; }
}
