using System.Collections.Generic;
using System.Threading.Tasks;
using Boxhead.Common;
using Boxhead.Domain.Models;
using Supabase.Postgrest.Exceptions;

namespace Boxhead.Infrastructure
{
    /// <inheritdoc cref="ICloudGameDatasource"/>
    public class SupabaseCloudGameDatasource : ICloudGameDatasource
    {
        private readonly Supabase.Client _supabaseClient;

        public SupabaseCloudGameDatasource(Supabase.Client supabaseClient) => _supabaseClient = supabaseClient;

        public async Task<List<Game>> GetAllGames()
        {
            try
            {
                var response = await _supabaseClient.From<Game>().Get();
                return response.Models;
            }
            catch (PostgrestException e)
            {
                throw new CloudException("Getting all games in infrastructure layer: " + e.Message, e);
            }
        }

        public async Task<Game> SaveGame(Game game)
        {
            try
            {
                var response = await _supabaseClient.From<Game>()
                                                    .Insert(game);
                return response.Model;
            }
            catch (PostgrestException e)
            {
                throw new CloudException("Saving game in infrastructure layer: " + e.Message, e);
            }
        }

        public async Task DeleteGame(Game game)
        {
            try
            {
                await _supabaseClient.From<Game>()
                                     .Where(x => x.Id == game.Id)
                                     .Delete();
            }
            catch (PostgrestException e)
            {
                throw new CloudException("Deleting game in infrastructure layer: " + e.Message, e);
            }
        }

        public async Task<Game> UpdateGame(Game game)
        {
            try
            {
                var response = await _supabaseClient.From<Game>()
                                                    .Update(game);
                return response.Model;
            }
            catch (PostgrestException e)
            {
                throw new CloudException("Updating game in infrastructure layer: " + e.Message, e);
            }
        }
    }
}
