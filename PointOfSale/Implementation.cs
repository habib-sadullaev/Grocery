namespace Grocery.PointOfSale;

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

internal class CartRepository : ICartRepository
{
    private Context context;
    private static int counter = 0;
    private static string CartKey(int cartId) => $"cart_{cartId}";

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

internal class CartItemRepository : ICartItemRepository
{
    private Context context;

    public CartItemRepository(Context context)
    {
        this.context = context;
    }

    private static string CartItemKey(int cartId, string productCode) => $"cartItem_{cartId}_{productCode}";
    private static string CartKey(int cartId) => $"cart_{cartId}";

    public void AddCartItems(Cart cart, Product product)
    {
        var cartItemkey = CartItemKey(cart.Id, product.Code);
        if (context.Store.TryGetValue(cartItemkey, out var obj) && obj is CartItem cartItem)
            cartItem.Amount += 1;
        else
        {
            cartItem = new CartItem(cart, product, 1);
            context.Store[cartItemkey] = cartItem;
            cart.Items.Add(cartItem);
        }
    }

    public IReadOnlyCollection<CartItem> GetCartItems(int cartId)
    {
        if (context.Store.TryGetValue(CartKey(cartId), out var obj) && obj is Cart cart)
            return cart.Items.ToList();

        return Array.Empty<CartItem>();
    }
}

internal class ProductRepository : IProductRepository
{
    private readonly Context context;

    public ProductRepository(Context context)
    {
        this.context = context;
    }

    public void Create(string productCode, decimal productPrice, int? discountAmount = null, decimal? discountPrice = null)
    {
        var key = ProductKey(productCode);
        if (context.Store.TryGetValue(key, out _))
            new InvalidOperationException($"a product with code '{productCode} already exists'");

        var product = new Product(productCode);
        var price = new ProductPrice(product, productPrice);
        product = product with { Price = price };

        if (discountAmount.HasValue && discountPrice.HasValue)
        {
            var discount = new ProductDiscount(product, discountAmount.Value, discountPrice.Value);
            product = product with { Discount = discount };
        }

        context.Store[key] = product;
    }

    private static string ProductKey(string productCode) => $"product_{productCode}";

    public Product Get(string productCode)
    {
        if (context.Store.TryGetValue(ProductKey(productCode), out var val) && val is Product product)
            return product;
        throw new InvalidOperationException($"a product with code '{productCode}' does not exist");
    }
}
