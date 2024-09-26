using System.Collections.Generic;
using Boxhead.Domain.Repositories;
using UnityEngine;
using Zenject;

namespace Boxhead.Presentation.LoadGame
{
    /// <summary>
    /// Manages the load game scene.
    /// </summary>
    /// <remarks>
    /// This scene is responsible for loading games from the cloud.
    /// </remarks>
    public class LoadGameManager : MonoBehaviour
    {
        [SerializeField] private Transform gameListParent;
        [SerializeField] private GameObject gameListItemPrefab;

        private IGameRepository _gameRepository;

        [Inject]
        public void Construct(IGameRepository gameRepository) => _gameRepository = gameRepository;

        private async void Awake()
        {
            var games = await _gameRepository.GetAllGames();
            if (games.IsFailure)
            {
                Debug.LogError(games.Error.Message);
                return;
            }

            FillGameList(games.Value);
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

        private static void OnGameSelected(Domain.Models.Game game) => CustomSceneManager.LoadGameScene(game);

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