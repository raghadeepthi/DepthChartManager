namespace DepthChartManager.Core.Dtos.Response
{
    public class PlayerPositionDto
    {
        public LeagueDto League { get; set; }

        public TeamDto Team { get; set; }

        public PlayerDto Player { get; set; }

        public SupportingPositionDto SupportingPosition { get; set; }

        public int SupportingPositionRanking { get; set; }
    }
}