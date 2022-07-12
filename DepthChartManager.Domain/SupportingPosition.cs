using DepthChartManager.Helpers;
using System;

namespace DepthChartManager.Domain
{
    public class SupportingPosition
    {
        public SupportingPosition(Guid leagueId, string name)
        {
            Contract.Requires<Exception>(!string.IsNullOrWhiteSpace(name), Resource.SupportPositionNameIsInvalid);

            Id = Guid.NewGuid();
            LeagueId = leagueId;
            Name = name;
        }

        public Guid Id { get; }

        public Guid LeagueId { get; }

        public string Name { get; }
    }
}