using System;
using System.Collections.Generic;
using Boxhead.Domain.Models;
using Boxhead.Domain.Repositories;
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
            IGameRepository gameRepository = new GameRepositoryImpl(cloudGameDatasource);

            int randomScore = UnityEngine.Random.Range(0, 100);
            int randomRound = UnityEngine.Random.Range(0, 100);
            Domain.Models.Game newGame = new(Guid.NewGuid(), DateTime.Now, new GameData(randomScore, randomRound));

            // var savedGame = await gameRepository.SaveGame(newGame);
            // if (savedGame.IsFailure)
            // {
            //     Debug.LogError(savedGame.Error.Message);
            //     return;
            // }

            // Debug.Log($"Game saved: {savedGame.Value.Id}");


            var games = await gameRepository.GetAllGames();
            if (games.IsFailure)
            {
                Debug.LogError(games.Error.Message);
                return;
            }

            foreach (var game in games.Value)
            {
                Debug.Log($"Game: {game.Id} -> {game.Data.Score}, {game.Data.Round}");
            }
        }

        public void StartNewGame() => SceneManager.LoadSceneAsync(GAME_SCENE_NAME);
        public void LoadGame() => Debug.Log("Load Game");
        public void NavigateToSettings() => Debug.Log("Navigate to Settings");
        public void QuitGame() => Application.Quit();
    }
}