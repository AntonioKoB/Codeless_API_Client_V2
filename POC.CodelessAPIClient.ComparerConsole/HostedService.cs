using Microsoft.Extensions.Hosting;

namespace POC.CodelessAPIClient.ComparerConsole;

public class HostedService : IHostedService
{
    private string _apiUrl;
    private char[] validChoices = new[] { '1', '2', '3' };
    private char[] testValidChoices = new[] { '4', '5', '6' };

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        char userChoice;
        Console.Write("api url: ");
        _apiUrl = Console.ReadLine();
        Console.Clear();
        do
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("||         Run the Client using:            ||");
            Console.WriteLine("==============================================");
            Console.WriteLine("1. HttpClient");
            Console.WriteLine("2. RestSharp");
            Console.WriteLine("3. Refit");
            Console.WriteLine("4. Test HttpClient Performance");
            Console.WriteLine("5. Test RestSharp Performance");
            Console.WriteLine("6. Test Refit Performance");
            Console.WriteLine("7. Compare All");
            Console.WriteLine("==============================================");
            Console.WriteLine("[Everything else will exit the application]");

            userChoice = Console.ReadKey().KeyChar;
            Console.Clear();

            if (!IsValidChoice(userChoice))
            {
                Console.WriteLine("Thanks for using the API Client");
                continue;
            }

            try
            {
                var commandRunner = HandleOptions(userChoice);
                if (validChoices.Contains(userChoice))
                    await commandRunner.Run();
                if (testValidChoices.Contains(userChoice))
                    await commandRunner.TestPerformance();
                if (userChoice == '7')
                    await RunAllTests();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        } while (IsValidChoice(userChoice));
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task RunAllTests()
    {
        await HandleOptions('4').TestPerformance(true);
        Console.WriteLine("==============================================");
        await HandleOptions('5').TestPerformance(true);
        Console.WriteLine("==============================================");
        await HandleOptions('6').TestPerformance(true);
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("Please press any key to continue");
        Console.ReadKey();
    }

    private RunCommandBase HandleOptions(char userChoice)
    {
        switch (userChoice)
        {
            case '1':
            case '4':
                return new RunCommandsUsingHttpClient(_apiUrl);
            case '2':
            case '5':
                return new RunCommandsUsingRestsharp(_apiUrl);
            case '3':
            case '6':
                return new RunCommandsUsingRefit(_apiUrl);

            default:
                return null; //will never happen
        }
    }

    private bool IsValidChoice(char choice)
    {
        return choice == '7' || validChoices.Concat(testValidChoices).Contains(choice);
    }
}