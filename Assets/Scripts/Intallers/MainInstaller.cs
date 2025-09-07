using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private GameObject player;

    public override void InstallBindings()
    {
        Container.Bind<GameData>()
            .FromNew()
            .AsSingle()
            .NonLazy();
        Container.Bind<PlayerData>()
            .FromNew()
            .AsSingle()
            .NonLazy();
        Container.Bind<IMaxScoreService>().
            To<PrefsMaxScoreService>()
            .AsSingle()
            .NonLazy();

        Container.Bind<PlayerMovement>()
            .FromComponentOn(player)
            .AsSingle();

        Container.Bind<PlayerCollector>()
            .FromComponentOn(player)
            .AsSingle();

        Container.Bind<IPositionService>()
            .To<PlayerPositionService>()
            .FromComponentOn(player)
            .AsSingle();

        Container.Bind<ControlsViewModel>().FromNew().AsSingle().NonLazy();
        Container.Bind<PauseWindowViewModel>().FromNew().AsSingle().NonLazy();
        Container.Bind<EndGameWindowViewModel>().FromNew().AsSingle().NonLazy();
    }
}