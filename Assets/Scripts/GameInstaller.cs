using Boxhead.Domain.Repositories;
using Boxhead.Infrastructure;
using Boxhead.Presentation.LoadGame;
using Boxhead.Presentation.StartMenu;
using CandyCoded.env;
using UnityEngine;
using Zenject;

namespace Boxhead
{
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

            Container.Bind<StartMenuManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<LoadGameManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}