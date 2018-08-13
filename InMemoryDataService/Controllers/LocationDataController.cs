using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using InMemoryDataService.Models;
using InMemoryDataService.Services;

namespace InMemoryDataService.Controllers
{
    public class LocationDataController : ApiController
    {
        public IEnumerable<Location> Get()
        {
            return InMemoryLocationData.CreateInMemoryLocationData();
        }
    }
}
