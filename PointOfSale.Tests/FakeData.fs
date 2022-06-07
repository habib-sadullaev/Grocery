[<AutoOpen>]
module FakeData

open System
open System.Collections.Generic
open System.Linq
open Grocery.PointOfSale

let ctx =
    let store = Dictionary<string, obj>()
    let mutable counter = 0
    let cartItemKey cartId productCode = $"cartItem_{cartId}_{productCode}"
    let cartKey cartId = $"cart_{cartId}"
    let productKey productCode = $"product_{productCode}"

    {
        new IUnitOfWork with
            member _.CartItems = 
                { new ICartItemRepository with
                      member _.AddCartItems(cart, product) =
                          let cartItemkey = cartItemKey cart.Id product.Code

                          match store.TryGetValue(cartItemkey) with
                          | true, (:? CartItem as cartItem) ->
                              cartItem.Amount <- cartItem.Amount + 1

                          | false, _ ->
                              let cartItem = CartItem(cart, product, 1)
                              store[cartItemkey] <- cartItem

                              let cart = store[cartKey cart.Id] :?> Cart
                              cart.Items.Add(cartItem)
                          
                          | true, _ ->
                              invalidOp "no such cart item"

                      member _.GetCartItems(cartId) =
                          match store.TryGetValue(cartKey cartId) with
                          | true, (:? Cart as cart) ->
                              upcast cart.Items.ToList()
                    
                          | false, _ ->
                              upcast Array.Empty()
                          
                          | true, o ->
                              if isNull o then "unreachable" else o.GetType().Name 
                              |> invalidOp }

            member _.Carts =
                { new ICartRepository with
                    member _.Create() =
                        counter <- counter + 1
                        let cart = Cart(counter, ResizeArray())
                        store.Add(cartKey counter, cart)

                        cart }
        
            member _.Products =
                { new IProductRepository with
                    member _.Get(productCode) =
                        match store.TryGetValue(productKey productCode) with
                        | true, (:? Product as product) ->
                            product

                        | _ ->
                            invalidOp $"a product with code '{productCode}' does not exist" 
                    
                    member _.Create(productCode, productPrice, discountAmount, discountPrice) =
                        match store.TryGetValue(productKey productCode) with
                        | false, _ ->
                            let product = Product(productCode, null, null) 
                            let price = ProductPrice(product, productPrice)
                            product.Price <- price

                            if discountAmount.HasValue && discountPrice.HasValue then
                                let discount = ProductDiscount(product, discountAmount.Value, discountPrice.Value)
                                product.Discount <- discount

                            store[productKey productCode] <- product

                        | true, _ ->
                            invalidOp $"a product with code '{productCode} already exists'" }
    }

do
    ctx.Products.Create("A", 1.25M, discountAmount = 3, discountPrice = 3.00M)
    ctx.Products.Create("B", 4.25M)
    ctx.Products.Create("C", 1.00M, discountAmount = 6, discountPrice = 5.00M)
    ctx.Products.Create("D", 0.75M)