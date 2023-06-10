using gpt3;

class Program
{
    static async Task Main(string[] args)
    {
        // Create the GPT3 object from LibGPT3 module.
        LibGPT3 gpt3 = new LibGPT3();

        await bucleAsync();

        async Task bucleAsync()
        {
            gpt3.MainMenu();
            Console.Clear();
            Console.Write($"{gpt3.label}Send mesage --> ");
            var userMsg = Console.ReadLine();
            do
            {
                if(userMsg == "clear")
                {
                    Console.Clear();
                }
                else
                {
                    string userMessage = string.Format("{0}'{1}'", gpt3.mainTask, userMsg);
                    // Call the SendChatRequest method
                    string response = await gpt3.SendChatRequest(userMessage);
                    // Print the API response
                    Console.WriteLine($"\nIA response:\n{response}");
                }
                Console.Write("\n");
                Console.Write($"{gpt3.label}Send mesage --> ");
                userMsg = Console.ReadLine();
            } while (userMsg != "exit");

            await bucleAsync();
        }
    }
} 