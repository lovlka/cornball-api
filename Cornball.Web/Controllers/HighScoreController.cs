using System;
using System.Linq;
using System.Web.Http;
using Cornball.Web.Models;
using Cornball.Web.Services;

namespace Cornball.Web.Controllers
{
    public class HighScoreController : ApiController
    {
        private readonly AzureService _azureService;

        public HighScoreController()
        {
            _azureService = new AzureService();
        }

        public DataRecord Get()
        {
            var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            return _azureService.GetHighscores(1, startDate, startDate.AddMonths(1)).FirstOrDefault();
        }

        public void Post(DataRecord dataRecord)
        {
            _azureService.SaveHighscore(dataRecord.Name, dataRecord.Value);
        }
    }
}