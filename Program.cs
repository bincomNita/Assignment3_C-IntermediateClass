using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main()
    {
        Console.WriteLine("Starting API calls...");

        List<string> list = new List<string>()
        {
            "Useful Fact","Programming Joke","Ninja Cat Fact"
        };


        var apiUrls = new List<string>
        {
            "https://uselessfacts.jsph.pl/random.json?language=en",
            "https://v2.jokeapi.dev/joke/Programming",
            "https://catfact.ninja/fact"
        };


        var tasks = new List<Task<string>>();
        foreach (var url in apiUrls)
        {
            tasks.Add(FetchDataAsync(url));
        }


        var results = await Task.WhenAll(tasks);


        for (int i = 0; i < results.Length; i++)
        {
            Console.WriteLine($"\nResponse from API {i + 1} about {list[i]}:\n{results[i]}\n");
        }

        Console.WriteLine("All API calls completed.");
    }

    static async Task<string> FetchDataAsync(string url)
    {
        try
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonData = await response.Content.ReadAsStringAsync();
            var formattedJson = JToken.Parse(jsonData).ToString();
            return formattedJson;
        }
        catch (Exception ex)
        {
            return $"Error fetching data from {url}: {ex.Message}";
        }
    }
}
