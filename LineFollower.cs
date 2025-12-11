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

        //Set SampleDataTime
        public static async Task SetSample()
        {
            //Post Request
            var payload = new 
            {
                sampleDeltaTime = 100
            };

            // JSON serialisieren
            string json = JsonSerializer.Serialize(payload);

           
            // In HttpContent umwandeln
            var content1 = new StringContent(json, Encoding.UTF8);

            content1.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response1 = await httpClient.PostAsync("http://192.168.4.1/api/agv/linefollower/setSampleDeltaTime", content1);

            Console.WriteLine(response1.StatusCode);
             
            if(response1.IsSuccessStatusCode)
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




       




        // read Data
    /*public static async void ReadSensors()
    {
        return null;
    }*/




}
