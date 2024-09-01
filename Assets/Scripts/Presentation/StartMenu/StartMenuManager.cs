using System;
using Boxhead.Infrastructure;
using CandyCoded.env;
using Supabase;
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

        private async void Awake()
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
            Domain.Models.Game game = new(Guid.NewGuid(), DateTime.Now, new Domain.Models.GameData(0, 0));

            try
            {
                var reseult = await cloudGameDatasource.SaveGame(game);
                var games = await cloudGameDatasource.GetAllGames();
                foreach (var g in games)
                {
                    Debug.Log(g.Id);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public void StartNewGame() => SceneManager.LoadSceneAsync(GAME_SCENE_NAME);
        public void LoadGame() => Debug.Log("Load Game");
        public void NavigateToSettings() => Debug.Log("Navigate to Settings");
        public void QuitGame() => Application.Quit();
    }
}