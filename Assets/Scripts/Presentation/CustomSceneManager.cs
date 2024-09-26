using UnityEngine.SceneManagement;

namespace Boxhead.Presentation
{
    /// <summary>
    /// Represents the scene manager.
    /// </summary>
    public class CustomSceneManager
    {
        private const string GAME_SCENE_NAME = "GameScene";
        private const string LOAD_GAME_SCENE_NAME = "LoadGameScene";
        private const string SETTINGS_SCENE_NAME = "SettingsScene";

        public static void LoadGameScene() => SceneManager.LoadSceneAsync(GAME_SCENE_NAME);
        public static void LoadLoadGameScene() => SceneManager.LoadSceneAsync(LOAD_GAME_SCENE_NAME);
        public static void LoadSettingsScene() => SceneManager.LoadSceneAsync(SETTINGS_SCENE_NAME);
    }
}