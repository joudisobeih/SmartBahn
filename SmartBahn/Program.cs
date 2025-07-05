
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace ApiBVG
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            string stopName = Console.ReadLine().ToLower().Replace(" ", "");
            string theUrl = $"https://v6.bvg.transport.rest/locations?query={stopName}&results=1";


            string locationId = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(theUrl);
                response.EnsureSuccessStatusCode();

                string returnString = await response.Content.ReadAsStringAsync();
                using JsonDocument doc = JsonDocument.Parse(returnString);
                JsonElement root = doc.RootElement;

                JsonElement firstItem = root[0];
                locationId = firstItem.GetProperty("id").GetString();
                Console.WriteLine($"Station ID: {locationId}");
                await File.WriteAllTextAsync("info.txt", returnString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            try
            {
                string depUrl = $"https://v6.bvg.transport.rest/stops/{locationId}/departures";
                HttpResponseMessage response2 = await client.GetAsync(depUrl);

                string returnDep = await response2.Content.ReadAsStringAsync();
                await File.WriteAllTextAsync("Dinfo.txt", returnDep);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            new JsonParse().json();

        }
    }

    class Departures
    {
        public string? when { get; set; }
        public string? plannedWhen { get; set; }
        public int? delay { get; set; }
    }

    class Root
    {
        public List<Departures>? departures { get; set; }
    }

    public class JsonParse
    {
        public void json()
        {
            string JsonPath = "Dinfo.txt";
            string JsonString = File.ReadAllText(JsonPath);

            Root data = JsonSerializer.Deserialize<Root>(JsonString);

            foreach (var dep in data.departures)
            {
                if (dep.delay >= 0)
                {
                    string plannedDep = dep.plannedWhen;
                    string actualDep = dep.when;
                    int delaytime = (dep.delay ?? 0) / 60;

                    string plannedDepTime = "";
                    string actualDepTime = "";

                    int tIndex = plannedDep.IndexOf('T');
                    int plusIndex = plannedDep.IndexOf('+');
                    int tIndex2 = actualDep.IndexOf('T');
                    int plusIndex2 = actualDep.IndexOf('+');

                    if (tIndex != -1 && plusIndex != -1 && plusIndex > tIndex)
                    {
                        plannedDepTime = plannedDep.Substring(tIndex + 1, plusIndex - tIndex - 1);
                        actualDepTime = actualDep.Substring(tIndex2 + 1, plusIndex2 - tIndex2 - 1);
                        Console.WriteLine(plannedDepTime);
                    }
                    Console.WriteLine($"Planned:{plannedDepTime}, Actual: {actualDepTime}, Delay: {delaytime}");
                }
                
            }
        }
    }

}
