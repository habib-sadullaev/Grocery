namespace Grocery.PointOfSale;

public abstract class CartCalculator
{
    protected readonly IUnitOfWork context;

    public CartCalculator(IUnitOfWork context)
    {
        this.context = context;
    }

    public abstract decimal Calculate(Cart cart);
}
