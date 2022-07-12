using System;


namespace DepthChartManager.Core.Dtos.Request
{
  public class RemovePlayerDto
  {
    public Guid LeagueId { get; set; }

    public Guid TeamId { get; set; }

    public string PlayerName { get; set; }

    public string PositionName { get; set; }
  }
}
