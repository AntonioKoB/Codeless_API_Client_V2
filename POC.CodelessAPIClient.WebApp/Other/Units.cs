namespace POC.CodelessAPIClient.WebApp.Other;

public class Units : List<Unit>
{
}

public class Unit
{
    public string Name { get; set; }
    public string Abrv { get; set; }
}

public class ShoppingListUnits
{
    public static Units GetUnits()
    {
        var units = new Units
        {
            new Unit { Name = "Liters", Abrv = "l" },
            new Unit { Name = "Kgs", Abrv = "kg" },
            new Unit { Name = "Units", Abrv = "" }
        };
        return units;
    }
}