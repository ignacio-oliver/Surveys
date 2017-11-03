using Surveys.Entities;
using System;
using System.Collections.Generic;
using Surveys.Web.DAL.SqlServer;
using System.Web.Http;
using System.Threading.Tasks;

namespace Surveys.Web.Controllers
{
    public class TeamsController : ApiController
    {
        private readonly TeamsProvider teamsProvider = new TeamsProvider();

        // GET: api/Teams
        public async Task<IEnumerable<Team>> Get()
        {
            var allTeams = await teamsProvider.GetAllTeamsAsync();
            var result = new List<Team>(allTeams);
            return result;
        }
    }
}
