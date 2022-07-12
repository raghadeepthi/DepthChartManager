using System;

namespace DepthChartManager.Core.Dtos.Request
{
    public class CreateSupportingPositionDto
    {
        public Guid LeagueId { get; set; }

        public string Name { get; set; }
    }
}