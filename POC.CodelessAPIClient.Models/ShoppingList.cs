namespace POC.CodelessAPIClient.Models;

public class ShoppingList : List<ShoppingListItem>
{
    public string Name { get; set; }
    public DateTime Date { get; set; }

    public void Print()
    {
        Console.WriteLine("================================================================");
        ForEach(item =>
        {
            item.Print();
        });

        Console.WriteLine("================================================================");
    }
}
