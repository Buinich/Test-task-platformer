using _Project.Code.Events;
using CodeBase.Data;
using CodeBase.Services.EventBus;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using UnityEngine;

namespace _Project.Code.Controllers
{
  public class KeysController : IKeysController
  {
    private readonly IStaticDataService _staticDataService;
    private readonly IEventBusService _eventBus;
    private KeysModel _keysModel;

    public KeysController(IStaticDataService staticDataService, IEventBusService eventBus, string levelName)
    {
      _staticDataService = staticDataService;
      _eventBus = eventBus;
      
      Initialize(levelName);
    }

    public void Initialize(string levelName)
    {
      LevelStaticData levelData = _staticDataService.ForLevel(levelName);
      if (levelData == null)
      {
        Debug.LogError($"THere is no LevelStaticData for {levelName}");
        return;
      }

      _keysModel = new KeysModel(levelData);
      _keysModel.OnKeysCountChanged += PublishKeysCountUpdated;
      _eventBus.Subscribe<KeyCollectedEvent>(OnKeyCollected);
      PublishKeysCountUpdated(_keysModel.KeysCollected, _keysModel.TotalKeys);
    }

    public void Cleanup()
    {
      if (_keysModel != null)
        _keysModel.OnKeysCountChanged -= PublishKeysCountUpdated;

      _eventBus.Unsubscribe<KeyCollectedEvent>(OnKeyCollected);
    }

    private void CheckAllKeysCollected()
    {
      if (_keysModel.KeysCollected >= _keysModel.TotalKeys)
        PublishAllKeysCollected();
    }

    public void OnKeyCollected(KeyCollectedEvent _)
    {
      _keysModel?.CollectKey();
      CheckAllKeysCollected();
    }

    private void PublishKeysCountUpdated(int currentKeys, int totalKeys)
    {
      _eventBus.Publish(new KeysCountUpdatedEvent(currentKeys, totalKeys));
    }

    private void PublishAllKeysCollected()
    {
      _eventBus.Publish(new AllKeysCollectedEvent());
    }
  }
}