using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;

namespace FillHackathon25
{
    internal class Motors
    {
        public static int s1;
        public static int s2;
        public static int s3;
        public static int s4;
        public static int s5;
        public static int velLeft = 20;
        public static int velRight = 20;

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



        public static async Task EnableMotors()
        {
            //Post Request
            var payload = new
            {
                stepper = "on"
            };

            // JSON serialisieren
            string json = JsonSerializer.Serialize(payload);


            // In HttpContent umwandeln
            var content1 = new StringContent(json, Encoding.UTF8);

            content1.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response1 = await httpClient.PostAsync("http://192.168.4.1/api/agv/stepper/enable", content1);
        }

        public static void KeepLine()
        {
            if (s2 > 30)
            {
                velLeft = 25;
                Console.WriteLine("LEFT");
            }
            if (s4 > 30)
            {
                velRight = 25;
                Console.WriteLine("RIGHT");

            }

        }

        public static void Stop()
        {
            if (s1 < 30 && s2 < 30 && s3 < 30 && s4 < 30 && s5 < 30)
            {
                velLeft = 0;
                velRight = 0;


            }
        }


        public static async Task SetVelocity()
        {
            string json = "";

            if (s2 < 30 && s4 < 30)
            {
                velLeft = 20;
                velRight = 20;
            }
            else if (s2 > 30 && s4 < 30)
            {
                velLeft = 30;

            }
            else if (s2 < 30 && s4 > 30)
            {
                velRight = 30;
            }

            //Post Request
            var payload_drive = new
            {
                velLeft_perc = velLeft,
                velRight_perc = velRight,
            };




            // JSON serialisieren
            json = JsonSerializer.Serialize(payload_drive);



            // In HttpContent umwandeln
            var content1 = new StringContent(json, Encoding.UTF8);

            content1.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response1 = await httpClient.PostAsync("http://192.168.4.1/api/agv/stepper/setVelocity", content1);
        }

        public static async Task GetCurrentVelocity()
        {


            // GET-Request
            var response = await httpClient.GetAsync("http://192.168.4.1/api/agv/stepper/getCurrentVelocity");

            string content = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Repsponse: " + content);


        }
    }
}
