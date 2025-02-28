using _Project.Code.Controllers;
using _Project.Code.Level.Loot;
using UnityEngine;
using Zenject;

namespace _Project.Code.Installers
{
  public class GameInstaller : MonoInstaller
  {
    [SerializeField] private ItemsSpawner ItemsSpawner;

    public override void InstallBindings()
    {
      Container.Bind<ItemsSpawner>()
        .FromInstance(ItemsSpawner)
        .AsSingle();

      Container.Bind<IKeysController>()
        .To<KeysController>()
        .AsSingle()
        .WithArguments(ItemsSpawner.SceneName ?? "Main")
        .NonLazy();

      Container.Bind<IHealthController>()
        .To<HealthController>()
        .AsSingle()
        .NonLazy();
    }
  }
}