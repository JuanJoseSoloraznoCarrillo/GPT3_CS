using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using gpt3;

class Program
{
    static async Task Main(string[] args)
    {
        // Create the GPT3 object from LibGPT3 module.
        LibGPT3 gpt3 = new LibGPT3();

        Console.WriteLine("What would you like to do?\n1-Translate.\n2-Grammar checker.\n3-Python expert.\n4-General propose.");
        var userInput = Console.ReadLine();
        string mainTask = string.Empty;
        string label = string.Empty;
        if (userInput.Contains("1"))
        {
            mainTask = "Translate ";
            label = "[Translation] ";
        }
        else if (userInput.Contains("2"))
        {
            mainTask = "Check the grammar of: ";
            label = "[Grammar] ";
        }
        else if (userInput.Contains("3"))
        {
            mainTask = "As python expert: ";
            label = "[Phyton] ";
        }
        else
        {
            mainTask = "";
            label = "[General] ";
        }

        Console.Write($"{label}Send mesage --> ");
        var userMsg = Console.ReadLine();
        string userMessage = string.Format("{0}'{1}'", mainTask, userMsg);
        Console.WriteLine($"You are sending ----> {userMessage}");
        // Call the SendChatRequest method
        string response = await gpt3.SendChatRequest(userMessage);

        // Print the API response
        Console.WriteLine(response);

        Console.ReadKey();
    }
}
