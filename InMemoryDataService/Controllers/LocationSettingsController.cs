using InMemoryDataService.Models;
using InMemoryDataService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace InMemoryDataService.Controllers
{
    public class LocationSettingsController : ApiController
    {
        public LocationSettings Get()
        {
            return InMemoryLocationSettings.CreateInMemoryLocationSettings();
        }
    }
}
