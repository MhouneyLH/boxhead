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
        /// <exception cref="CloudException">If something went wrong.</exception>
        Task<List<Game>> GetAllGames();

        /// <summary>
        /// Saves a game to the cloud.
        /// </summary>
        /// <param name="game">The game to save.</param>
        /// <returns>The saved game. (e. g. the Guid is generated in the cloud directly normally.)</returns>
        /// <exception cref="CloudException">If something went wrong.</exception>
        Task<Game> SaveGame(Game game);

        /// <summary>
        /// Updates a game in the cloud.
        /// </summary>
        /// <param name="game">The game to update.</param>
        /// <returns>The updated game.</returns>
        /// <exception cref="CloudException">If something went wrong.</exception>
        Task<Game> UpdateGame(Game game);

        /// <summary>
        /// Deletes a game from the cloud.
        /// </summary>
        /// <param name="game">The game to delete.</param>
        /// <exception cref="CloudException">If something went wrong.</exception>
        Task DeleteGame(Game game);
    }
}
