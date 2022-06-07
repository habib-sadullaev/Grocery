module Tests

open Xunit
open Grocery.PointOfSale
open Grocery.PointOfSale.Implementations

let context = Context.createContext()
let calulator : CartCalculator = CartItemDiscountCalculator(context) 
let terminal = PointOfSaleTerminal(context, calulator)

context.Products.Create("A", 1.25M, discountAmount = 3, discountPrice = 3.00M)
context.Products.Create("B", 4.25M)
context.Products.Create("C", 1.00M, discountAmount = 6, discountPrice = 5.00M)
context.Products.Create("D", 0.75M)

let inline testTotalPrice products expectedTotalPrice =
    let cart = terminal.CreateCart()
    
    for productCode in products do
        terminal.Scan(cart, string productCode)

    let actualTotalPrice = terminal.CalculateTotal(cart)

    Assert.Equal(expectedTotalPrice, actualTotalPrice)

[<Fact>]
let ``Scan these items in this order: ABCDABA; Verify the total price is $13,25`` () =
    testTotalPrice "ABCDABA" 13.25M

[<Fact>]
let ``Scan these items in this order: CCCCCCC; Verify the total price is $6,00`` () =
    testTotalPrice "CCCCCCC" 6.00M

[<Fact>]
let ``Scan these items in this order: ABCD; Verify the total price is $7,25`` () =
    testTotalPrice "ABCD" 7.25M
    