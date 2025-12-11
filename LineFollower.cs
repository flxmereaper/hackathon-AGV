using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Net.Mime;

namespace FillHackathon25
{

    internal class LineFollower
    {
        public static HttpClient httpClient = new HttpClient();

        public static void Init()
        {

            // Setzen der Basis-Adresse des Servers
            httpClient.BaseAddress = new Uri(Program.BaseUrl);

            // Setzen des API-Keys als Bearer Token für die Authentifizierung
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Program.ApiKey);

            // Setzen des Accept-Headers, um JSON-Antworten zu erwarten
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task GetData()
        {

            // API-Key hinzufügen

            var payload = new
            {
                sampleDeltaTime = 100

            };

            var json = JsonSerializer.Serialize(payload);



            var content1 = new StringContent(json, Encoding.UTF8, "application/json");

            // GET-Request
            var response = await httpClient.GetAsync("http://192.168.4.1/api/agv/linefollower/sensors");

            string content = await response.Content.ReadAsStringAsync();

            //Console.WriteLine("Status: " + response.StatusCode);

            int startIndex = content.IndexOf("[") - 1;
            string goal = content.Substring(startIndex + 1);
            goal = goal.Replace("}", "");

            int[] arr = JsonSerializer.Deserialize<int[]>(goal);

            //Console.WriteLine(string.Join(", ", arr));

            Motors.s1 = arr[0];
            Motors.s2 = arr[1];
            Motors.s3 = arr[2];
            Motors.s4 = arr[3];
            Motors.s5 = arr[4];

            //Console.WriteLine(string.Join(", ", arr));

            // Console.WriteLine($"s2: {Motors.s2}, s4: {Motors.s4}");

        }



        //Set SampleDataTime
        public static async Task SetSample()
        {
            //Post Request
            var payload = new
            {
                sampleDeltaTime = 50
            };

            // JSON serialisieren
            string json = JsonSerializer.Serialize(payload);


            // In HttpContent umwandeln
            var content1 = new StringContent(json, Encoding.UTF8);

            content1.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response1 = await httpClient.PostAsync("http://192.168.4.1/api/agv/linefollower/setSampleDeltaTime", content1);


            Console.WriteLine(response1.StatusCode);

            if (response1.IsSuccessStatusCode)
            {
                Console.WriteLine("Erfolgreich");
            }
            else
            {
                string errorDetails = await response1.Content.ReadAsStringAsync();

                // Statuscode und Fehlerdetails ausgeben
                Console.WriteLine($"❌ Nicht geändert. Statuscode: {response1.StatusCode}");
                Console.WriteLine($"Fehlerdetails: {errorDetails}");
            }

        }



        //LineSensor Enable
        public static async Task EnableSensor()
        {
            //Post Request
            var payload = new
            {
                enable = true
            };

            // JSON serialisieren
            string json = JsonSerializer.Serialize(payload);

            var content = new StringContent(json, Encoding.UTF8);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync("http://192.168.4.1/api/agv/linefollower/enable", content);

            Console.WriteLine("Enabled ");

            Console.WriteLine(response.StatusCode);

            Console.ReadLine();

        }
    }
}
