using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WhereToEat.Services
{
    public class ZomatoServices : FoodStatsService
    {
        public override HttpResponseMessage GetCategories()
        {
            throw new NotImplementedException();
        }

        public override HttpResponseMessage GetCitiesByCoordinates(string lat, string lon)
        {
            throw new NotImplementedException();
        }

        public override HttpResponseMessage GetCollections()
        {
            throw new NotImplementedException();
        }
    }
}
