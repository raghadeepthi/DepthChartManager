using System;

namespace DepthChartManager.Core.Dtos.Response
{
    public class SupportingPositionDto
    {
        public Guid Id { get; set; }

        public Guid LeagueId { get; set; }

        public string Name { get; set; }
    }
}