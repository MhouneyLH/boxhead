using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boxhead.Common;
using Boxhead.Domain.Models;
using Boxhead.Infrastructure;
using CSharpFunctionalExtensions;

namespace Boxhead.Domain.Repositories
{
    /// <inheritdoc cref="IGameRepository"/>
    public class GameRepositoryImpl : IGameRepository
    {
        private readonly ICloudGameDatasource _cloudGameDatasource;

        public GameRepositoryImpl(ICloudGameDatasource cloudGameDatasource) => _cloudGameDatasource = cloudGameDatasource;

        public async Task<IResult<List<Game>, Error>> GetAllGames()
        {
            try
            {
                // todo: currently when reading in the wrong format (e. g. wrong table name), nothing is thrown; it justs sets the column value to null and returns a success result with the null value -> this is NOT ideal because of null reference exceptions that can occur later on
                var games = await _cloudGameDatasource.GetAllGames();
                return Result.Success<List<Game>, Error>(games.ToList());
            }
            catch (CloudException e)
            {
                return Result.Failure<List<Game>, Error>(Error.Create("Failed to get games", e.Message));
            }
        }

        public async Task<IResult<Game, Error>> SaveGame(Game game)
        {
            try
            {
                var savedGame = await _cloudGameDatasource.SaveGame(game);
                return Result.Success<Game, Error>(savedGame);
            }
            catch (CloudException e)
            {
                return Result.Failure<Game, Error>(Error.Create("Failed to save game", e.Message));
            }
        }

        public async Task<IResult<Error>> DeleteGame(Game game)
        {
            try
            {
                await _cloudGameDatasource.DeleteGame(game);
                return Result.Success(null as Error);
            }
            catch (CloudException e)
            {
                return Result.Failure<Error>("Failed to delete game" + e.Message);
            }
        }

        public async Task<IResult<Game, Error>> UpdateGame(Game game)
        {
            try
            {
                var updatedGame = await _cloudGameDatasource.UpdateGame(game);
                return Result.Success<Game, Error>(updatedGame);
            }
            catch (CloudException e)
            {
                return Result.Failure<Game, Error>(Error.Create("Failed to update game", e.Message));
            }
        }
    }
}