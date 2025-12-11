using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;



namespace FillHackathon25
{
    internal class Program
    {
        public static string BaseUrl = "http://192.168.4.1"; // Die IP-Adresse Ihres ESP32
        public const string ApiKey = "bORUlc3kORVGeLwx";
        static async Task Main(string[] args)
        { 
            Console.WriteLine($"Starte Verbindungstest zu {BaseUrl}...");

            LineFollower.Init();

            await LineFollower.SetSample();

            await LineFollower.EnableSensor();

            Console.ReadLine();

        }
    }
}
