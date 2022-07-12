using System;

namespace DepthChartManager.Core.Dtos.Request
{
    public class GetSupportingPositionDto
    {
        public Guid LeagueId { get; set; }

        public string SupportingPositionName { get; set; }
    }
}