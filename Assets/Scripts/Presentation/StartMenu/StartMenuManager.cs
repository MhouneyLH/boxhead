using System;
using Boxhead.Domain.Models;
using Boxhead.Domain.Repositories;
using Boxhead.Infrastructure;
using CandyCoded.env;
using Supabase;
using UnityEngine;

namespace Boxhead.Presentation.StartMenu
{
    /// <summary>
    /// Manages the start menu scene.
    /// Mostly this scene is responsible as gateway for the whole game.
    /// </summary>
    public class StartMenuManager : MonoBehaviour
    {
        public static void StartNewGame() => CustomSceneManager.LoadGameScene();
        public static void LoadGame() => CustomSceneManager.LoadLoadGameScene();
        public static void NavigateToSettings() => CustomSceneManager.LoadSettingsScene();
        public static void QuitGame() => Application.Quit();
    }
}