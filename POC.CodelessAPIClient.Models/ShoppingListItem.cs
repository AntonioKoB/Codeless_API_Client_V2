namespace POC.CodelessAPIClient.Models;

public class ShoppingListItem
{
    public Guid? Id { get; set; }
    public int Quantity { get; set; }
    public string Unit { get; set; } //l, kg, etc. Null or empty means units
    public string UnitName { get; set; } //Liters, Kgs, Units, etc
    public string Name { get; set; }
    public string? Description { get; set; } //anything. It can be a barcode, or other usefull info. Optional
    public bool IsBought { get; set; } //for checkbox UserInterface, whenever we want to mark it as bought.

    public void Print()
    {
        var serObject =
            $@"
Id:              {Id},
Quantity:        {Quantity},
Unit:            '{Unit}',
UnitName:        {UnitName},
Name:            {Name},
IsBought:        {(IsBought?"Yes":"No")},
";
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine(serObject);
        Console.WriteLine("-----------------------------------------");
    }

}