using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthChartManager.Domain
{
  public class FullDepthPlayer
  {

    public FullDepthPlayer(string positionName, IEnumerable<Player> players )
    {
      PositionName = positionName;
      Players = players.ToList();

    }
    public string PositionName { get; set; }
    public IEnumerable<Player> Players { get; set; }
     

  }
}
