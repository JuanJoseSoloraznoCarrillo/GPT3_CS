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
        HttpClient? client;
        public LibGPT3() 
        {
            apiUrl = "https://api.openai.com/v1/chat/completions";
            apiKey = "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";
            model  = "gpt-3.5-turbo"; // Specify the desired model version
            // Create an instance of HttpClient
            client = new HttpClient();
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
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            // Send the request and get the response
            var response = await client.PostAsync(apiUrl, content);

            // Read the response content
            var responseContent = await response.Content.ReadAsStringAsync();
            // Parse the response content as a JSON object
            JObject responseObject = JObject.Parse(responseContent);
            var responseOne = responseObject.Last.ToString();
            var finalResponse = responseOne.Split("content")[1].Split('}')[0];
            
            return finalResponse;
        }


    }
}
