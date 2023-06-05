using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        // Create an instance of HttpClient
        HttpClient client = new HttpClient();

        // Define the API endpoint URL and your API key
        string apiUrl = "https://api.openai.com/v1/chat/completions";
        string apiKey = "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";
        string model = "gpt-3.5-turbo"; // Specify the desired model version
        // Call the SendChatRequest method
        string userMessage = string.Join(" ", args);
        string response = await SendChatRequest(client, apiUrl, apiKey, model, userMessage);

        // Print the API response
        Console.WriteLine(response);

        Console.ReadKey();
    }

    static async Task<string> SendChatRequest(HttpClient client, string apiUrl, string apiKey, string model, string message = "hello")
    {
        // Prepare the request data
        var requestData = new
        {
            messages = new[] { new { role = "system", content = "You are a helpful assistant." }, new { role = "user", content = message } },
            model = model
        };
        // Serialize the request data
        var content = new StringContent(JsonConvert.SerializeObject(requestData), new UTF8Encoding(false), "application/json");

        // Add the API key to the request headers
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

        // Send the request and get the response
        var response = await client.PostAsync(apiUrl, content);

        // Read the response content
        var responseContent = await response.Content.ReadAsStringAsync();
        // Parse the response content as a JSON object
        JObject responseObject = JObject.Parse(responseContent);
        var responseOne = responseObject.Last.ToString();
        var finalResponse = responseOne.Split("content")[1].Split('}')[0];
        
        return finalResponse.Split(":")[1];
    }
}
