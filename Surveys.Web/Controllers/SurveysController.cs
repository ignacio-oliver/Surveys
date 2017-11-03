using Surveys.Entities;
using Surveys.Web.DAL.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Surveys.Web.Controllers
{
    public class SurveysController : ApiController
    {
        private readonly SurveysProvider surveysProvider = new SurveysProvider();

        // GET: api/Surveys
        public async Task<IEnumerable<Survey>> Get()
        {
            var allSurveys = await surveysProvider.GetAllSurveysAsync();
            return allSurveys;
        }

        // POST: api/Surveys
        public async Task Post([FromBody]IEnumerable<Survey> surveys)
        {
            if(surveys == null)
            {
                return;
            }
            foreach (var survey in surveys)
            {
                await surveysProvider.InsertSurveyAsync(survey);
            }
        }
    }
}
