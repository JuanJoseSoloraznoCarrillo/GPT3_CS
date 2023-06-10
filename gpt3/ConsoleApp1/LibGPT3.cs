using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace gpt3
{
    public class LibGPT3
    {
        // Define the API endpoint URL and your API key
        string apiUrl = string.Empty;
        string apiKey = string.Empty;
        string model = string.Empty; 
        internal string mainTask = string.Empty;
        internal string label = string.Empty;
        HttpClient? client;
        public LibGPT3() 
        {
            apiUrl = "https://api.openai.com/v1/chat/completions";
            apiKey = "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";
            model  = "gpt-3.5-turbo"; // Specify the desired model version
            // Create an instance of HttpClient
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        }

        public async Task<string> SendChatRequest(string message = "hello")
        {
            // Prepare the request data
            var requestData = new
            {
                messages = new[] { new { role = "system", content = "Hello, can you help me?." }, new { role = "user", content = message } },
                model = model
            };
            // Serialize the request data
            var content = new StringContent(JsonConvert.SerializeObject(requestData), new UTF8Encoding(false), "application/json");
            // Add the API key to the request headers
            // Send the request and get the response
            var response = await client.PostAsync(apiUrl, content);
            // Read the response content
            var responseContent = await response.Content.ReadAsStringAsync();
            // Parse the response content as a JSON object, and then to a string to have only the IA response.
            return this.ParseJObject(JObject.Parse(responseContent));
        }

        internal void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("What would you like to do?\n1-Translate.\n2-Grammar checker.\n3-Python expert.\n4-Dictionary.\n5-General propose.");
            Console.Write("\nPutn the number of your answer -> ");
            var userInput = Console.ReadLine();
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
            else if (userInput.Contains("4"))
            {
                mainTask = "Explain me, give synonyms and examples of the word: ";
                label = "[Dictionary] ";
            }
            else if (userInput.Contains("5"))
            {
                mainTask = "";
                label = "[General] ";
            }
            else if (userInput.Contains("exit"))
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Chose a correct one !!!");
                Thread.Sleep(1500);
                this.MainMenu();
            }

        }
        private string ParseJObject(JObject json_object)
        {
            Dictionary<string,JToken> responseDict = json_object.ToObject<Dictionary<string,JToken>>();
            Dictionary<string, object> choice = responseDict["choices"][0].ToObject<Dictionary<string, object>>();
            JObject ia_msg = JObject.Parse(choice["message"].ToString());
            return ia_msg["content"].ToString().Replace("\n", "").Replace("\"", "");
        }
    }
}
