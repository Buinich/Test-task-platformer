using System;
using _Project.Code.Events;
using _Project.Code.Infrastructure.Factory;
using _Project.Code.Infrastructure.SceneManagement;
using CodeBase.Services.EventBus;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using Eflatun.SceneReference;
using UnityEngine;
using Zenject;

namespace _Project.Code.Level.Loot
{
  public class ItemsSpawner : MonoBehaviour
  {
    public string SceneName => Scene.Name;

    [SerializeField] private SceneReference Scene;

    private IGameFactory _factory;
    private IStaticDataService _staticData;
    private IEventBusService _eventBus;
    private LevelStaticData _levelData;
    private ISceneLoader _sceneLoader;

    [Inject]
    public void Construct(IGameFactory factory, IStaticDataService staticData, IEventBusService eventBus
      , ISceneLoader sceneLoader)
    {
      _factory = factory;
      _staticData = staticData;
      _eventBus = eventBus;
      _sceneLoader = sceneLoader;
    }

    private void Start()
    {
      SpawnItems();
    }

    private void SpawnItems()
    {
      _levelData = _staticData.ForLevel(Scene.Name);
      if (_levelData == null)
      {
        Debug.LogError($"There is no LevelStaticData for {Scene.Name}");
        return;
      }

      SpawnKeys(_levelData);
      SpawnHealthPacks(_levelData);
    }

    private void SpawnKeys(LevelStaticData levelData)
    {
      foreach (Vector3 spawner in levelData.KeySpawners)
      {
        LootPiece key = _factory.CreateLoot(spawner, transform);
        key.OnCollected += PublishKeyCollected;
      }
    }

    private void SpawnHealthPacks(LevelStaticData levelData)
    {
      foreach (Vector3 spawner in levelData.HealthPackSpawners)
      {
        LootPiece healthPack = _factory.CreateHealth(spawner, transform);
        healthPack.OnCollected += PublishHealthPackCollected;
      }
    }

    private void PublishKeyCollected()
    {
      _eventBus.Publish(new KeyCollectedEvent());
    }

    private void PublishHealthPackCollected()
    {
      _eventBus.Publish(new HealthRestoredEvent(_levelData.HealthPackHeal));
    }
  }
}