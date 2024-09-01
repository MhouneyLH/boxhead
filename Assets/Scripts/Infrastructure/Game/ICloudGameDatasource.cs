using System.Collections.Generic;
using System.Threading.Tasks;
using Boxhead.Domain.Models;

namespace Boxhead.Infrastructure
{
    /// <summary>
    /// Represents the datasource when being online.
    /// This datasource is used to interact with the cloud.
    /// </summary>
    public interface ICloudGameDatasource
    {
        /// <summary>
        /// Gets all games from the cloud.
        /// </summary>
        /// <returns>A list of games.</returns>
        Task<List<Game>> GetAllGames();

        /// <summary>
        /// Saves a game to the cloud.
        /// </summary>
        /// <param name="game">The game to save.</param>
        /// <returns>The saved game. (e. g. the Guid is generated in the cloud directly normally.)</returns>
        Task<Game> SaveGame(Game game);
    }
}
