using System.Collections.Generic;
using System.Threading.Tasks;
using Boxhead.Domain.Repositories;
using Boxhead.Infrastructure;
using CandyCoded.env;
using Supabase;
using UnityEngine;

namespace Boxhead.Presentation.LoadGame
{
    public class LoadGameManager : MonoBehaviour
    {
        [SerializeField] private Transform gameListParent;
        [SerializeField] private GameObject gameListItemPrefab;

        private IGameRepository _gameRepository;

        private async void Awake()
        {
            await Setup();
            if (_gameRepository == null)
            {
                Debug.LogError("Game repository not initialized.");
                return;
            }

            var games = await _gameRepository.GetAllGames();
            if (games.IsFailure)
            {
                Debug.LogError(games.Error.Message);
                return;
            }

            FillGameList(games.Value);
        }

        private async Task Setup()
        {
            if (!env.TryParseEnvironmentVariable("SUPABASE_URL", out string url) ||
                !env.TryParseEnvironmentVariable("SUPABASE_KEY", out string key))
            {
                Debug.LogError("Supabase URL or Key not found in environment variables.");
                return;
            }

            SupabaseOptions options = new()
            {
                AutoConnectRealtime = true
            };

            Client supabase = new(url, key, options);
            await supabase.InitializeAsync();

            ICloudGameDatasource cloudGameDatasource = new SupabaseCloudGameDatasource(supabase);
            _gameRepository = new GameRepositoryImpl(cloudGameDatasource);
        }

        private void FillGameList(List<Domain.Models.Game> games)
        {
            foreach (var game in games)
            {
                GameObject createdGame = Instantiate(gameListItemPrefab, gameListParent);
                var component = createdGame.GetComponent<GameListItemComponent>();
                component.Initialize(game);
                component.OnSelect += OnGameSelected;
                component.OnDelete += OnGameDeleted;
            }
        }

        private void OnGameSelected(Domain.Models.Game game)
        {
            Debug.Log("Selected: " + game.Id);
        }

        private async void OnGameDeleted(Domain.Models.Game game)
        {
            var deleteResponse = await _gameRepository.DeleteGame(game);
            if (deleteResponse.IsFailure)
            {
                Debug.LogError(deleteResponse.Error);
                return;
            }

            Debug.Log("Deleted: " + game.Id);
        }
    }
}