namespace Grocery.PointOfSale;

using Grocery.PointOfSale.Interfaces;
using Grocery.PointOfSale.Models;

public abstract class CartCalculator
{
    protected readonly IUnitOfWork context;

    public CartCalculator(IUnitOfWork context)
    {
        this.context = context;
    }

    public abstract decimal Calculate(Cart cart);
}
