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

            var response = await httpClient.PostAsync("http://192.168.4.1/api/agv/pwm/enable", content);

            Console.WriteLine(response.StatusCode);

            Console.ReadLine();
        }

        public static async Task SetServoChanel()
        {
            //Post Request
            var payload = new
            {
                activeServos = 4
            };

            // JSON serialisieren
            string json = JsonSerializer.Serialize(payload);


            // In HttpContent umwandeln
            var content = new StringContent(json, Encoding.UTF8);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync("http://192.168.4.1/api/agv/pwm/setActiveServos", content);

            Console.WriteLine(response.StatusCode);

            Console.ReadLine();
        }


        public static async Task SetPosition()
        {
            //Post Request
            var payload = new
            {
                channel = 0,
                newPosition = 50
            };

            // JSON serialisieren
            string json = JsonSerializer.Serialize(payload);


            // In HttpContent umwandeln
            var content = new StringContent(json, Encoding.UTF8);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync("http://192.168.4.1/api/agv/pwm/setPosition", content);

            Console.WriteLine(response.StatusCode);

            Console.ReadLine();

        }

        public static async Task SetServoLimits()
        {
            //Post Request
            var payload = new
            {
                channel = 0,
                min = 0,
                max = 170
            };

            // JSON serialisieren
            string json = JsonSerializer.Serialize(payload);


            // In HttpContent umwandeln
            var content = new StringContent(json, Encoding.UTF8);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync("http://192.168.4.1/api/agv/pwm/setLimits", content);

            Console.WriteLine(response.StatusCode);

            Console.ReadLine();
        }

        public static async Task SetStartPosition()
        {
            //Post Request
            var payload = new
            {
                channel = 0,
                startPos = 90
            };

            // JSON serialisieren
            string json = JsonSerializer.Serialize(payload);


            // In HttpContent umwandeln
            var content = new StringContent(json, Encoding.UTF8);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync("http://192.168.4.1/api/agv/pwm/setStartPosition", content);

            Console.WriteLine(response.StatusCode);

            Console.ReadLine();
        }

        public static async Task SetSweepTime()
        {
            //Post Request
            var payload = new
            {
                channel = 0,
                fullSweepTime_ms = 1000
            };

            // JSON serialisieren
            string json = JsonSerializer.Serialize(payload);


            // In HttpContent umwandeln
            var content = new StringContent(json, Encoding.UTF8);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync("http://192.168.4.1/api/agv/pwm/setFullSweepTime", content);

            Console.WriteLine(response.StatusCode);

            Console.ReadLine();
        }



            



    }
}
