using System.Collections.Generic;
using System.Web.Http;
using Cornball.Web.Models;
using Cornball.Web.Services;

namespace Cornball.Web.Controllers
{
    public class StatisticsController : ApiController
    {
        private readonly AzureService _azureService;

        public StatisticsController()
        {
            _azureService = new AzureService();
        }

        public IEnumerable<DataRecord> Get()
        {
            return _azureService.GetStatistics();
        }

        public void Post(DataRecord dataRecord)
        {
            _azureService.IncreaseValue(dataRecord.Name);
        }
    }
}