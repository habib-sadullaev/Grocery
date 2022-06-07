namespace Grocery.PointOfSale;

public class PointOfSaleTerminal
{
    private readonly IUnitOfWork context;
    private readonly CartCalculator cartCalculator;

    public PointOfSaleTerminal(IUnitOfWork context, CartCalculator cartCalculator)
    {
        this.context = context;
        this.cartCalculator = cartCalculator;
    }

    public Cart CreateCart()
    {
        return context.Carts.Create();
    }

    public void Scan(Cart cart, string productCode)
    {
        var product = context.Products.Get(productCode);
        context.CartItems.AddCartItems(cart, product);
    }

    public decimal CalculateTotal(Cart cart)
    {
        return cartCalculator.Calculate(cart);
    }
}
