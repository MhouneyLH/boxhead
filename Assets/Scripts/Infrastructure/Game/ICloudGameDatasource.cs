using System.Collections.Generic;
using System.Threading.Tasks;
using Boxhead.Domain.Models;

namespace Boxhead.Infrastructure
{
    public interface ICloudGameDatasource
    {
        Task<List<Game>> GetAllGames();
        Task<Game> SaveGame(Game game);
    }
}
