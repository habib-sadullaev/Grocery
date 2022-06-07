namespace Grocery.PointOfSale;

public class PointOfSaleTerminal
{
    private readonly IUnitOfWork ctx;
    private readonly CartCalculator cartCalculator;

    public PointOfSaleTerminal(IUnitOfWork ctx, CartCalculator cartCalculator)
    {
        this.ctx = ctx;
        this.cartCalculator = cartCalculator;
    }

    public Cart CreateCart()
    {
        return ctx.Carts.Create();
    }

    public void Scan(Cart cart, string productCode)
    {
        var product = ctx.Products.Get(productCode);
        ctx.CartItems.AddCartItems(cart, product);
    }

    public decimal CalculateTotal(Cart cart)
    {
        return cartCalculator.Calculate(cart);
    }
}
