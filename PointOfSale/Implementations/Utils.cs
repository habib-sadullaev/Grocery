namespace Grocery.PointOfSale.Implementations;

internal static class Utils
{
    public static string ProductKey(string productCode) => $"product_{productCode}";
    public static string CartKey(int cartId) => $"cart_{cartId}";
    public static string CartItemKey(int cartId, string productCode) => $"cartItem_{cartId}_{productCode}";
}
