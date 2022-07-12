using DepthChartManager.Helpers;
using System;
using System.Collections.Generic;

namespace DepthChartManager.Domain
{
    public class League
    {
        private List<Team> _teams = new List<Team>();

        private List<SupportingPosition> _supportingPositions = new List<SupportingPosition>();

        public League(string name)
        {
            Contract.Requires<Exception>(!string.IsNullOrWhiteSpace(name), Resource.LeagueNameIsInvalid);

            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; }

        public string Name { get; }

        public IEnumerable<Team> Teams => _teams.AsReadOnly();

        public IEnumerable<SupportingPosition> SupportingPositions => _supportingPositions.AsReadOnly();

        public Team GetTeam(Guid id)
        {
            return _teams.Find(t => t.Id == id);
        }

        public Team AddTeam(string name)
        {
            Contract.Requires<Exception>(!string.IsNullOrWhiteSpace(name), Resource.TeamNameIsInvalid);
            Contract.Requires<Exception>(!_teams.Exists(t => string.Equals(t.Name, name, StringComparison.OrdinalIgnoreCase)), Resource.TeamAlreadyExists);

            var team = new Team(this, name);
            _teams.Add(team);
            return team;
        }

        public SupportingPosition AddSupportingPosition(string name)
        {
            Contract.Requires<Exception>(!string.IsNullOrWhiteSpace(name), Resource.SupportPositionNameIsInvalid);
            Contract.Requires<Exception>(!_supportingPositions.Exists(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase)), Resource.SupportPositionAlreadyExists);

            var supportingPosition = new SupportingPosition(Id, name);
            _supportingPositions.Add(supportingPosition);
            return supportingPosition;
        }
    }
}