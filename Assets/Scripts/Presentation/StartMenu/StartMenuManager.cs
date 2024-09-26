using Boxhead.Domain.Repositories;
using UnityEngine;
using Zenject;

namespace Boxhead.Presentation.StartMenu
{
    /// <summary>
    /// Manages the start menu scene.
    /// Mostly this scene is responsible as gateway for the whole game.
    /// </summary>
    public class StartMenuManager : MonoBehaviour
    {
        private IGameRepository _gameRepository;

        [Inject]
        public void Construct(IGameRepository gameRepository) => _gameRepository = gameRepository;

        public async void StartNewGame()
        {
            var newGame = Domain.Models.Game.CreateNew();
            var createdGame = await _gameRepository.SaveGame(newGame);
            if (createdGame.IsFailure)
            {
                Debug.LogError("Failed to save new game: " + createdGame.Error);
                return;
            }

            CustomSceneManager.LoadGameScene(createdGame.Value);
        }

        public static void LoadGame() => CustomSceneManager.LoadLoadGameScene();
        public static void NavigateToSettings() => CustomSceneManager.LoadSettingsScene();
        public static void QuitGame() => Application.Quit();
    }
}