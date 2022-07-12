
using System.Collections.Generic;

namespace DepthChartManager.Core.Dtos.Response
{
  public class FullDepthChartDto
  {
    public string PositionName { get; set; }
    public IEnumerable<PlayerDto> Players { get; set; }
  }
}
