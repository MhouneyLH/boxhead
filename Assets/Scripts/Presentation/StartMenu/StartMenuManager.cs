using UnityEngine;
using UnityEngine.SceneManagement;

namespace Boxhead.Presentation.StartMenu
{
    /// <summary>
    /// Manages the start menu scene.
    /// Mostly this scene is responsible as gateway for the whole game.
    /// </summary>
    public class StartMenuManager : MonoBehaviour
    {
        private const string GAME_SCENE_NAME = "GameScene";

        public void StartNewGame() => SceneManager.LoadSceneAsync(GAME_SCENE_NAME);
        public void LoadGame() => Debug.Log("Load Game");
        public void NavigateToSettings() => Debug.Log("Navigate to Settings");
        public void QuitGame() => Application.Quit();
    }
}