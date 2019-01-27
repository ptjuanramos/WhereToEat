using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WhereToEat.Services
{
    public abstract class FoodStatsService
    {
        public abstract HttpResponseMessage GetCategories();

        public abstract HttpResponseMessage GetCollections();

        public abstract HttpResponseMessage GetCitiesByCoordinates(string lat, string lon);

        public async Task<HttpResponseMessage> CallService(HttpMethod httpMethod, string callUri, Dictionary<string, string> headers)
        {
            using(HttpClient httpClient = new HttpClient())
            {
                var httpRequestMessage = new HttpRequestMessage
                {
                    Method = httpMethod,
                    RequestUri = new Uri(callUri)
                };

                PopulateHeaders(httpRequestMessage, headers);

                return await httpClient.SendAsync(httpRequestMessage);
            }
        }

        private void PopulateHeaders(HttpRequestMessage httpRequestMessage, Dictionary<string,string> headers)
        {
            foreach(string headerKey in headers.Keys)
            {
                string headerValue = headers[headerKey];
                httpRequestMessage.Headers.Add(headerKey, headerValue);
            }
        }

        //More methods need to be implemented - GetRestaurants for example
        //HttpResponseMessage
    }
}
