using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using CSharp_CallOpenWeatherAPIAssignment.Models;

namespace CSharp_CallOpenWeatherAPIAssignment.BusinessLogic
{
    public class WeatherBL
    {
        private const string API_KEY = "e80758081f2bf960047caffb2c97ab3a";
        private const string CurrentWeatherUrl =
            "http://api.openweathermap.org/data/2.5/weather?" +
            "@QUERY@=@LOC@&units=imperial&APPID=" +
            API_KEY;

        public async Task<ForecastModel> GetWeatherAsync(string zip)
        {
            //Sample API Call:
            //  api.openweathermap.org/data/2.5/weather?zip=94040,us          

            //Format our URL to tell it we are searching by zip and pass the zip
            string url = CurrentWeatherUrl.Replace("@QUERY@", "zip");
            url = url.Replace("@LOC@", zip);
            
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    ForecastModel forecastModel = JsonConvert.DeserializeObject<ForecastModel>(jsonString, settings);

                    return forecastModel;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
