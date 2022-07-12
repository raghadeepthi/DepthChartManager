using System;

namespace DepthChartManager.Core.Dtos.Response
{
    public class PlayerDto
    {
        public Guid Id { get; set; }

        public LeagueDto League { get; set; }

        public TeamDto Team { get; set; }

        public string PlayerName { get; set; }
    }
}
