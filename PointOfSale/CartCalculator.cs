namespace Grocery.PointOfSale;

public abstract class CartCalculator
{
    protected readonly IUnitOfWork ctx;

    public CartCalculator(IUnitOfWork ctx)
    {
        this.ctx = ctx;
    }

    public abstract decimal Calculate(Cart cart);
}
