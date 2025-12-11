using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace FillHackathon25
{
    internal class Part
    {
        public int id { get; set; }
        public string purchased { get; set; }
        public int locationId { get; set; }
    }
    internal class BackendRequest
    {
        private static readonly HttpClient Client = new HttpClient();
        private static readonly string baseUrl = "http://10.230.16.45:3000"; //Bei neuem Verbinden ändern!
        public static async Task<Dictionary<int, bool>> IsEnabled()
        {
            var dict = new Dictionary<int, bool>();
            var response = await Client.GetAsync($"{baseUrl}/enabled");
            string content = await response.Content.ReadAsStringAsync();
            if(content.Equals("true"))
            {
                var stationResponse = await Client.GetAsync($"{baseUrl}/orders");
                string stationContent = await stationResponse.Content.ReadAsStringAsync();
                foreach (JsonObject item in JsonObject.Parse(stationContent) as JsonArray)
                {
                    var curr = JsonSerializer.Deserialize<Part>(item);
                    dict.Add(curr.locationId, curr.purchased.Equals("true"));
                };
                return dict;
            }
            return null;
        }

        public static async Task StationDocked(int stationId)
        {
            var payload = new
            {
                availableItems = 1,
                collectedItems = 0.5
            };

            var jsonPayload = JsonSerializer.Serialize(payload);
            var payloadContent = new StringContent(jsonPayload, Encoding.UTF8);
            payloadContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var response = await Client.PutAsync($"{baseUrl}/locations/{stationId}", payloadContent);

            Console.WriteLine(response.StatusCode);
        }

        public static async Task StationDone(int stationId)
        {
            var payload = new
            {
                availableItems = 1,
                collectedItems = 1
            };

            var jsonPayload = JsonSerializer.Serialize(payload);
            var payloadContent = new StringContent(jsonPayload, Encoding.UTF8);
            payloadContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var response = await Client.PutAsync($"{baseUrl}/locations/{stationId}", payloadContent);

            Console.WriteLine(response.StatusCode);
        }
    }
}
