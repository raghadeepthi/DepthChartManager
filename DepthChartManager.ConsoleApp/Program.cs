using DepthChartManager.Core.Interfaces.Repositories;
using DepthChartManager.Core.Interfaces.Services;
using DepthChartManager.Core.Services;
using DepthChartManager.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepthChartManager.ConsoleApp
{
    public class Program
    {
        private IServiceProvider _serviceProvider;

        private static void Main(string[] args)
        {
            Task.Run(async () => await new Program().ConfigureServices().Run()).Wait();
        }

        private Program ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(ISportRepository).Assembly);
            services.AddAutoMapper(typeof(ISportRepository).Assembly);
            services.AddSingleton<ISportRepository, SportRepository>();
            services.AddSingleton<IDepthChartService, DepthChartService>();

            _serviceProvider = services.BuildServiceProvider(); // Build the container now
            return this;
        }

        private async Task Run()
        {
            var depthChartService = _serviceProvider.GetService<IDepthChartService>();

            // Add league
            var nfl = await depthChartService.AddLeague("NFL");

            // Add supporting positions
            foreach (var supportingPositionName in new List<string> { "LWR", "RWR", "LT", "LG", "C", "RG", "RT", "TE", "QB", "RB" })
            {
                await depthChartService.AddSupportingPosition(nfl.Id, supportingPositionName);
            }

            // Add team
            var buffaloBills = await depthChartService.AddTeam(nfl.Id, "Buffalo Bills");

            // Add players to QB position
            var i = 0;
            foreach (var playerName in new[] { "Alice", "Bob", "Charlie" })
            {
                
                await depthChartService.AddPlayer(nfl.Id, buffaloBills.Id, playerName, "QB", i );
                i++;
            }

            var alice = await depthChartService.GetPlayer(nfl.Id, buffaloBills.Id, "Alice");
            var bob = await depthChartService.GetPlayer(nfl.Id, buffaloBills.Id, "Bob");
            var charlie = await depthChartService.GetPlayer(nfl.Id, buffaloBills.Id, "Charlie");

            var quarterBackReceiverPosition = await depthChartService.GetSupportingPosition(nfl.Id, "QB");
            var leftReceiverPosition = await depthChartService.GetSupportingPosition(nfl.Id, "LT");

            // Update player positions
            await depthChartService.UpdatePlayerPosition(nfl.Id, buffaloBills.Id, bob.Id, quarterBackReceiverPosition.Id, 0);
            await depthChartService.UpdatePlayerPosition(nfl.Id, buffaloBills.Id, charlie.Id, quarterBackReceiverPosition.Id, 2);
            await depthChartService.UpdatePlayerPosition(nfl.Id, buffaloBills.Id, alice.Id, quarterBackReceiverPosition.Id, 0);
            await depthChartService.UpdatePlayerPosition(nfl.Id, buffaloBills.Id, bob.Id, leftReceiverPosition.Id, 0);

            var playerPositions = await depthChartService.GetPlayerPositions(nfl.Id, buffaloBills.Id);

            foreach (var supportingPositionInfo in playerPositions.GroupBy(p => p.SupportingPosition.Name))
            {
                Console.WriteLine($@"{supportingPositionInfo.Key}: [{string.Join(",", supportingPositionInfo.Select(s => $"{s.Player.PlayerName}"))}]");
            }

            var backupPlayerPositions = await depthChartService.GetBackupPlayerPositions(nfl.Id, buffaloBills.Id, "Alice", "QB");

            Console.WriteLine($@"Back up players for {alice.PlayerName}");

            foreach (var backPlayerPosition in backupPlayerPositions)
            {
                Console.WriteLine($@"{backPlayerPosition.PlayerName}");
            }

            //Full Depth for Alice player
            var fullDepthPlayers =  await depthChartService.GetFullDepthChart(nfl.Id, buffaloBills.Id);

            Console.WriteLine($@"Full Depth");

           
            var qbPlayers = fullDepthPlayers.Where(c => c.PositionName == "QB").ToList();

            foreach (var playerForAllPosition in fullDepthPlayers)
            {
              foreach (var player in playerForAllPosition.Players)
              {
                 Console.WriteLine($@" {playerForAllPosition.PositionName} - {player.Id},{player.PlayerName}");
              }
            }
          
        }
    }
}
