using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthChartManager.Core.Dtos.Request
{
  public class GetFullDepthPlayersDto
  {
    public Guid LeagueId { get; set; }

    public Guid TeamId { get; set; }
  }
}
