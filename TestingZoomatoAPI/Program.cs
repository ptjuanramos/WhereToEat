using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestingZoomatoAPI
{
    class Program
    {
        const string ENDPOINT = "https://developers.zomato.com/api/v2.1/";
        const string APIKEY = "cdfae4ce150a1793086f9177fb1fedb4";

        static void Main(string[] args)
        {
            string[] picoasCoords = { "38.7312026", "-9.1482463" };
            ListOfCategories listOfCategories = GetZomatoCategories();
            Console.WriteLine("List of categories Response: " + JsonConvert.SerializeObject(listOfCategories) + "\n");

            CitiesResponse citiesResponse = GetCitiesInCoords(picoasCoords[0], picoasCoords[1]);
            Console.WriteLine("Cities Response: " + JsonConvert.SerializeObject(citiesResponse) + "\n");
            Console.WriteLine("Locations sugestions: " + JsonConvert.SerializeObject(citiesResponse.location_suggestions) + "\n");

            Collections zoomatoCollection = GetCollections(lat: picoasCoords[0], lon: picoasCoords[1]);
            Console.WriteLine("Collections Response: " + JsonConvert.SerializeObject(zoomatoCollection) + "\n");

            Console.ReadKey();
        }

        private static async Task<HttpResponseMessage> CallGetZoomatoAPI(string callUri)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(callUri),
                    Headers = {
                        { "user-key", APIKEY }
                    }
                };

                return await httpClient.SendAsync(httpRequestMessage);
            }
        }


        //Webservices calls

        public static ListOfCategories GetZomatoCategories()
        {
            var requestUrl = ENDPOINT + "categories";
            var response = CallGetZoomatoAPI(requestUrl).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<ListOfCategories>(responseString);
            }

            //TODO - after the error handling should be implemented
            return null;
        }

        public static CitiesResponse GetCitiesInCoords(string lat, string lon)
        {
            var requestUrl = ENDPOINT + $"cities?lat={lat}&lon={lon}";
            var response = CallGetZoomatoAPI(requestUrl).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<CitiesResponse>(responseString);
            }

            //TODO - after the error handling should be implemented
            return null;
        }

        public static Collections GetCollections(string cityId = null, string lat = null, string lon = null, int count = 100) 
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if (cityId != null) parameters.Add("city_id", cityId);
            if (lat != null) parameters.Add("lat", lat);
            if (lon != null) parameters.Add("lon", lon);
            parameters.Add("count", count + "");

            string requestUrl = Utility.PrepareRequestUrl(ENDPOINT + "collections?", parameters);
            var response = CallGetZoomatoAPI(requestUrl).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Collections>(responseString);
            }

            return null;
        }

        //public static
    }

    public class Utility
    {
        public static string PrepareRequestUrl(string requestUrl, Dictionary<string, string> parameters)
        {
            string result = "";
            foreach(string key in parameters.Keys)
            {
                string value = parameters[key];
                result += key + "=" + value + "&";
            }

            return requestUrl + result.Remove(result.Length - 1);
        }
    }

    // -------
    public class ListOfCategories
    {
        public Category[] categories { get; set; }
    }

    public class Category
    {
        public Categories categories { get; set; }
    }

    public class Categories
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    // -------

    public class CitiesResponse
    {
        public LocationSugestion[] location_suggestions { get; set; }
        public string status { get; set; }
        public int has_more { get; set; }
        public int has_total { get; set; }
    }

    public class LocationSugestion
    {
        public int id { get; set; }
        public string name { get; set; }
        public int country_id { get; set; }
        public string country_name { get; set; }
        public string country_flag_url { get; set; }
        public int should_experiment_with { get; set; }
        public int discovery_enabled { get; set; }
        public int has_new_ad_format { get; set; }
        public int is_state { get; set; }
        public int state_id { get; set; }
        public string state_name { get; set; }
        public string state_code { get; set; }
    }

    // -------

    public class Collections
    {
        public Collection[] collections { get; set; }
        public int has_more { get; set; }
        public string share_url { get; set; }
        public string display_text { get; set; }
        public int has_total { get; set; }
    }

    public class Collection
    {
        public CollectionInfo collection { get; set; }
    }

    public class CollectionInfo
    {
        public int collection_id { get; set; }
        public int res_count { get; set; }
        public string image_url { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string share_url { get; set; }
    }

    // -------

    //TODO add in different handler class
    //This is just a POC and some classes preparation
    public class Response<T>
    {
        public T Message { get; set; }
        public bool Ok { get; set; }
    }

    public class ErrorResponse
    {
        public string code { get; set; }
        public string status { get; set; }
        public string forbidden { get; set; }
        public string message { get; set; }
    }
}