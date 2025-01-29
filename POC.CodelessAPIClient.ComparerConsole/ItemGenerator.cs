using POC.CodelessAPIClient.Models;

namespace POC.CodelessAPIClient.ComparerConsole;

public static class ItemGenerator
{
    public static IEnumerable<ShoppingListItem> Generate()
    {
        yield return new ShoppingListItem()
        {
            Name = "Banana",
            Quantity = 5,
            UnitName = "Units",
            Unit = "",
            IsBought = false
        };

        yield return new ShoppingListItem()
        {
            Name = "Apple",
            Quantity = 5,
            UnitName = "Units",
            Unit = "",
            IsBought = false
        };

        yield return new ShoppingListItem()
        {
            Name = "Letuce",
            Quantity = 2,
            UnitName = "Units",
            Unit = "",
            IsBought = false
        };

        yield return new ShoppingListItem()
        {
            Name = "Rice",
            Quantity = 2,
            UnitName = "Kilogram",
            Unit = "Kg",
            IsBought = false
        };

        yield return new ShoppingListItem()
        {
            Name = "Milk",
            Quantity = 2,
            UnitName = "Pint",
            Unit = "pt",
            IsBought = false
        };

        yield return new ShoppingListItem()
        {
            Name = "Bread",
            Quantity = 5,
            UnitName = "Units",
            Unit = "",
            IsBought = false
        };

        yield return new ShoppingListItem()
        {
            Name = "Peanut butter",
            Quantity = 1,
            UnitName = "Units",
            Unit = "",
            IsBought = false
        };

        yield return new ShoppingListItem()
        {
            Name = "Eggs",
            Quantity = 1,
            UnitName = "Box",
            Unit = "Box",
            IsBought = false
        };

        yield return new ShoppingListItem()
        {
            Name = "Cheese",
            Quantity = 1,
            UnitName = "Units",
            Unit = "",
            IsBought = false
        };

        yield return new ShoppingListItem()
        {
            Name = "Yogurt",
            Quantity = 1,
            UnitName = "Kilogram",
            Unit = "kg",
            IsBought = false
        };

        yield return new ShoppingListItem()
        {
            Name = "Beef",
            Quantity = 1,
            UnitName = "Kilogram",
            Unit = "kg",
            IsBought = false
        };

        yield return new ShoppingListItem()
        {
            Name = "Tuna",
            Quantity = 1,
            UnitName = "Pack",
            Unit = "pck",
            IsBought = false
        };

        yield return new ShoppingListItem()
        {
            Name = "Passata",
            Quantity = 1,
            UnitName = "Botle",
            Unit = "btl",
            IsBought = false
        };

        yield return new ShoppingListItem()
        {
            Name = "Wine",
            Quantity = 1,
            UnitName = "Botle",
            Unit = "btl",
            IsBought = false
        };
    }
}