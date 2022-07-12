using System;

namespace DepthChartManager.Core.Dtos.Request
{
    public class CreateTeamDto
    {
        public Guid LeagueId { get; set; }

        public string Name { get; set; }
    }
}