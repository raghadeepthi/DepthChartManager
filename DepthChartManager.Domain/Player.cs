using DepthChartManager.Helpers;
using System;

namespace DepthChartManager.Domain
{
    public class Player
    {
        public Player(League league, Team team, string playerName, string positionName, int positionDepth)
        {
            Contract.Requires<Exception>(league != null, Resource.LeagueNameIsInvalid);
            Contract.Requires<Exception>(team != null, Resource.TeamNameIsInvalid);
            Contract.Requires<Exception>(!string.IsNullOrWhiteSpace(playerName), Resource.PlayerNameIsInvalid);

            Id = Guid.NewGuid();
            League = league;
            Team = team;
            PlayerName = playerName;
            PositionName = positionName;
            PositionDepth = positionDepth;

        }

        public Guid Id { get; }

        public League League { get; }

        public Team Team { get; }

        public string PlayerName { get; set; }

        public string PositionName { get; set; }

        public int PositionDepth { get; set; }
    }
}
