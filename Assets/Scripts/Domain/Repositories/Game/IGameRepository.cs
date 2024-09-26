using System.Collections.Generic;
using System.Threading.Tasks;
using Boxhead.Common;
using Boxhead.Domain.Models;
using CSharpFunctionalExtensions;

namespace Boxhead.Domain.Repositories
{
    /// <summary>
    /// Represents the repository for games.
    /// </summary>
    public interface IGameRepository
    {
        /// <summary>
        /// Gets all games.
        /// </summary>
        /// <returns>A list of games or an error, if something went wrong.</returns>
        /// <seealso cref="ICloudGameDatasource.GetAllGames()"/>
        Task<IResult<List<Game>, Error>> GetAllGames();

        /// <summary>
        /// Saves a game.
        /// </summary>
        /// <param name="game">The game to save.</param>
        /// <returns>The saved game or an error, if something went wrong.</returns>
        /// <remarks>
        /// Normally, the Guid is generated in the cloud directly.
        /// </remarks>
        /// <seealso cref="ICloudGameDatasource.SaveGame(Game)"/>
        Task<IResult<Game, Error>> SaveGame(Game game);

        /// <summary>
        /// Deletes a game.
        /// </summary>
        /// <param name="game">The game to delete.</param>
        /// <returns>Nothing or an error, if something went wrong.</returns>
        /// <seealso cref="ICloudGameDatasource.DeleteGame(Game)"/>
        Task<IResult<Error>> DeleteGame(Game game);
    }
}