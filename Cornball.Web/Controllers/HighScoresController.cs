using System;
using System.Collections.Generic;
using System.Web.Http;
using Cornball.Web.Models;
using Cornball.Web.Services;

namespace Cornball.Web.Controllers
{
    public class HighScoresController : ApiController
    {
        private readonly AzureService _azureService;

        public HighScoresController()
        {
            _azureService = new AzureService();
        }

        public IEnumerable<DataRecord> Get(DateTime? startDate = null, DateTime? endDate = null)
        {
            return _azureService.GetHighscores(10, startDate, endDate);
        }
    }
}