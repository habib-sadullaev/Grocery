namespace Grocery.PointOfSale;

public class CartItemDiscountCalculator : CartCalculator
{
    public CartItemDiscountCalculator(IUnitOfWork ctx) : base(ctx) { }

    public override decimal Calculate(Cart cart)
    {
        return ctx.CartItems.GetCartItems(cart.Id).Sum(Calculate);
    }

    private decimal Calculate(CartItem cartItem)
    {
        if (cartItem.Product.Discount != null)
        {
            var (quotient, remainder) = Math.DivRem(cartItem.Amount, cartItem.Product.Discount.Amount);
            var totalWithDiscount = quotient * cartItem.Product.Discount.Price + remainder * cartItem.Product.Price.Price;
            return totalWithDiscount;
        }

        var total = cartItem.Amount * cartItem.Product.Price.Price;
        return total;
    }
}
