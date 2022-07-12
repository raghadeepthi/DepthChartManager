using System;

namespace DepthChartManager.Core.Dtos.Request
{
    public class GetPlayerPositionDto
    {
        public Guid LeagueId { get; set; }

        public Guid TeamId { get; set; }
    }
}