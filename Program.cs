using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Timers;



namespace FillHackathon25
{
    internal class Program
    {
        static Timer timer = new Timer(10); // 100 ms


        public static string BaseUrl = "http://192.168.4.1"; // Die IP-Adresse Ihres ESP32
        public const string ApiKey = "bORUlc3kORVGeLwx";
        static async Task Main(string[] args)
        {
            await Servo.EnableServo();

            timer.Elapsed += TimerElapsed;
            timer.AutoReset = true;
            timer.Start();


            Console.WriteLine($"Starte Verbindungstest zu {BaseUrl}...");

            LineFollower.Init();

            await LineFollower.SetSample();

            await LineFollower.EnableSensor();

            await Servo.EnableServo();

            await Servo.SetStartPosition();


            await Motors.EnableMotors();



            await Motors.GetCurrentVelocity();

            //await LineFollower.GetData();

            Console.ReadLine();

        }

        public static async void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            await LineFollower.GetData();
            //Motors.KeepLine();
            await Motors.SetVelocity();
            //Motors.Stop();



            //Stop();
        }
    }
}
