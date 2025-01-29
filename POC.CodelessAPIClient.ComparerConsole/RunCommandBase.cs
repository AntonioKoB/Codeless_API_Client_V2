using System.Diagnostics;
using POC.CodelessAPIClient.Models;

namespace POC.CodelessAPIClient.ComparerConsole;

public abstract class RunCommandBase
{
    private readonly string _commandRunnerType;

    protected string ApiBaseURL { get; private set; }

    public RunCommandBase(string apiBaseURL, string runnerType)
    {
        ApiBaseURL = apiBaseURL;
        _commandRunnerType = runnerType;
    }

    public async Task Run()
    {
        Console.Clear();
        Console.WriteLine($"Running api client using {_commandRunnerType}");
        Console.WriteLine("Populating database");
        await PopulateDatabase();
        Console.WriteLine("Done");
        var shoppingList = await GetShoppingList();
        shoppingList.Print();

        Console.WriteLine("Select one ID to make it marked as bought");
        var idStr = Console.ReadLine();
        if (Guid.TryParse(idStr, out Guid id))
        {
            var item = shoppingList.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                item.IsBought = true;
                await Update(item);
            }
        }

        (await GetShoppingList()).Print();

        Console.WriteLine("Press any key to clear the database...");
        Console.ReadKey();
        var updatedList = await Delete();

        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("Finished");
        Console.WriteLine("");
        Console.WriteLine("");
    }

    public async Task TestPerformance(bool runForAll = false)
    {
        if (!runForAll) Console.Clear();

        Console.WriteLine($"Testing Performance when using {_commandRunnerType}");

        var stopWatch = new Stopwatch();
        stopWatch.Start();
        await PopulateDatabase();
        var shoppingList = await GetShoppingList();
        stopWatch.Stop();

        //get one
        var item = shoppingList.FirstOrDefault();
        item.IsBought = true;

        stopWatch.Start();
        await Update(item);
        await Delete();
        stopWatch.Stop();
        Console.WriteLine(stopWatch.Elapsed.ToString(@"mm\:ss\.ff"));
    }

    public abstract Task PopulateDatabase();
    public abstract Task<ShoppingList> GetShoppingList();
    public abstract Task<ShoppingList> Update(ShoppingListItem item);
    public abstract Task<ShoppingList> Delete();
}