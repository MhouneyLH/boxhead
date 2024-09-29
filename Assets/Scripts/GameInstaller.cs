using Boxhead.Domain.Repositories;
using Boxhead.Infrastructure;
using Boxhead.Presentation.Game;
using Boxhead.Presentation.LoadGame;
using Boxhead.Presentation.StartMenu;
using CandyCoded.env;
using UnityEngine;
using Zenject;

namespace Boxhead
{
    /// <summary>
    /// Installer for everything related to a game.
    /// (Especially the repository and the managers that use the repository.)
    /// </summary>
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            if (!env.TryParseEnvironmentVariable("SUPABASE_URL", out string url) ||
                !env.TryParseEnvironmentVariable("SUPABASE_KEY", out string key))
            {
                Debug.LogError("Failed to parse SUPABASE_URL or SUPABASE_KEY from environment variables.");
                return;
            }

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            var client = new Supabase.Client(url, key, options);
            // todo: async did not work at this place but it's like 5am in the morning and I'm tired
            client.InitializeAsync().Wait(0);

            Container.Bind<Supabase.Client>().FromInstance(client).AsSingle();
            Container.Bind<ICloudGameDatasource>().To<SupabaseCloudGameDatasource>().AsSingle();
            Container.Bind<IGameRepository>().To<GameRepositoryImpl>().AsSingle();

            // these are the managers that are in the scene where the repository is used
            Container.Bind<StartMenuManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<LoadGameManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}