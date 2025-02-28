using System.Threading.Tasks;
using _Project.Code.Controllers;
using _Project.Code.Infrastructure.AssetManagement;
using _Project.Code.Infrastructure.Factory;
using _Project.Code.Infrastructure.SceneManagement;
using _Project.Code.Level.Loot;
using CodeBase.Services.EventBus;
using CodeBase.Services.Input;
using CodeBase.Services.StaticData;
using UnityEngine;
using Zenject;

namespace _Project.Code.Infrastructure.Bootstrap
{
  public class BootstrapInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private SceneLoader SceneLoader;
    
    private SceneLoader _sceneLoader;
    
    public override void InstallBindings()
    {
      Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
      
      BindSceneLoader();

      BindServices();
    }

    public void Initialize()
    {
      Container.Resolve<IStaticDataService>().Load();
      LoadScene();
    }

    private void BindSceneLoader()
    {
      _sceneLoader = Container.InstantiatePrefabForComponent<SceneLoader>(SceneLoader);
      Container.Bind<ISceneLoader>().FromInstance(_sceneLoader).AsSingle();
    }

    private void BindServices()
    {
      Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
      Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
      Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
      Container.Bind<IEventBusService>().To<EventBusService>().AsSingle();
      Container.Bind<IInputService>().To<InputService>().AsSingle();
    }

    private async void LoadScene()
    {
      await _sceneLoader.LoadSceneGroupAsync(0);
    }
  }
}