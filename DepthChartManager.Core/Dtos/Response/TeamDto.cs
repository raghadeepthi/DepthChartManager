using System;

namespace DepthChartManager.Core.Dtos.Response
{
    public class TeamDto
    {
        public Guid Id { get; set; }

        public LeagueDto League { get; set; }

        public string Name { get; set; }
    }
}