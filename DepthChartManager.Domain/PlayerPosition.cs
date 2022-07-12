using DepthChartManager.Helpers;
using System;

namespace DepthChartManager.Domain
{
    public class PlayerPosition
    {
        public PlayerPosition(League league, Team team, Player player, SupportingPosition supportingPosition, int supportingPositionRanking)
        {
            Contract.Requires<Exception>(league != null, Resource.LeagueNameIsInvalid);
            Contract.Requires<Exception>(team != null, Resource.TeamNameIsInvalid);
            Contract.Requires<Exception>(league != null, Resource.PlayerNameIsInvalid);
            Contract.Requires<Exception>(team != null, Resource.SupportPositionNameIsInvalid);

            League = league;
            Team = team;
            Player = player;
            SupportingPosition = supportingPosition;
            SupportingPositionRanking = supportingPositionRanking;
        }

        public League League { get; }

        public Team Team { get; }

        public Player Player { get; }

        public SupportingPosition SupportingPosition { get; }

        public int SupportingPositionRanking { get; }
    }
}