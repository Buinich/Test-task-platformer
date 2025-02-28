using _Project.Code.Views;
using UnityEngine;
using Zenject;

namespace _Project.Code.Installers
{
  public class UIInstaller : MonoInstaller
  {
    [SerializeField] private KeysView KeysView;
    [SerializeField] private HealthView HealthView;

    public override void InstallBindings()
    {
      Container.Bind<KeysView>()
        .FromInstance(KeysView)
        .AsSingle()
        .NonLazy();

      Container.Bind<HealthView>()
        .FromInstance(HealthView)
        .AsSingle()
        .NonLazy();
    }
  }
}