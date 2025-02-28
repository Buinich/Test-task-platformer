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

namespace _Project.Code.Player
{
  public class PlayerSpawner : MonoBehaviour
  {
    [SerializeField] private SceneReference Scene;

    private IGameFactory _factory;
    private IStaticDataService _staticData;
    private IEventBusService _eventBus;
    private PlayerController _currentPlayer;

    [Inject]
    public void Construct(IGameFactory factory, IStaticDataService staticData, IEventBusService eventBus)
    {
      _factory = factory;
      _staticData = staticData;
      _eventBus = eventBus;
    }

    private void Start()
    {
      SpawnPlayer();
    }

    private void OnEnable()
    {
      _eventBus.Subscribe<PlayerDiedEvent>(RespawnPlayer);
    }

    private void OnDisable()
    {
      _eventBus.Unsubscribe<PlayerDiedEvent>(RespawnPlayer);
    }

    private void SpawnPlayer()
    {
      LevelStaticData levelData = _staticData.ForLevel(Scene.Name);
      if (levelData == null)
      {
        Debug.LogError($"There is no LevelStaticData for {Scene.Name}");
        return;
      }

      CreatePlayerAtStartPosition(levelData);
    }
    
    private void RespawnPlayer(PlayerDiedEvent evt)
    {
      DestroyCurrentPlayer();
      _eventBus.Publish(new HealthResetEvent());
      CreatePlayerAtStartPosition(_staticData.ForLevel(Scene.Name));
    }

    private void CreatePlayerAtStartPosition(LevelStaticData levelData)
    {
      _currentPlayer = _factory.CreatePlayer(levelData.PlayerSpawner);
      NotifyCamera(_currentPlayer.gameObject);
    }
    
    private void DestroyCurrentPlayer()
    {
      if (_currentPlayer != null)
      {
        Destroy(_currentPlayer.gameObject);
        _currentPlayer = null;
      }
    }

    private void NotifyCamera(GameObject player)
    {
      _eventBus.Publish(new PlayerSpawnedEvent(player));
    }
  }
}