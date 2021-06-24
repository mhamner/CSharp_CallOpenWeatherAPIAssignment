using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_CallOpenWeatherAPIAssignment.Models;
using CSharp_CallOpenWeatherAPIAssignment.BusinessLogic;

namespace CSharp_CallOpenWeatherAPIAssignment
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Calls the Open Weather API
            bool keepGoing = true;

            //Initialize our http client to get ready for our API call
            ApiHelper.InitializeClient();

            while (keepGoing)
            {
                Console.WriteLine("** WEATHER **");
                Console.Write("Please enter a Zipcode to get weather for:  ");

                string zipCode = Console.ReadLine();

                //Call the API
                WeatherBL weatherBL = new WeatherBL();
                ForecastModel forecastModel = await weatherBL.GetWeatherAsync(zipCode);

                Console.WriteLine("");
                Console.WriteLine($"Current weather for {zipCode}:");
                Console.WriteLine($"Temperature:  {forecastModel.main.temp.ToString()}");
                Console.WriteLine($"Feel Like:  {forecastModel.main.feels_like.ToString()}");
                Console.WriteLine("Current Conditions:");
                foreach(Weather weather in forecastModel.weather)
                {
                    Console.WriteLine(weather.description);
                }

                Console.Write("Do another [Y/N]?  ");
                keepGoing = (Console.ReadLine().ToLower() == "y") ? true : false;
            }
        }
    }
}
