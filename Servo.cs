using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace FillHackathon25
{
    internal class Servo
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

        public static async Task EnableServo()
        {
            //Post Request
            var payload = new
            {
                enable = true
            };

            // JSON serialisieren
            string json = JsonSerializer.Serialize(payload);


            // In HttpContent umwandeln
            var content = new StringContent(json, Encoding.UTF8);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync("http://192.168.4.1/api/agv/linefollower/setSampleDeltaTime", content);

            Console.WriteLine(response.StatusCode);

            Console.ReadLine();
        }
    }
}
