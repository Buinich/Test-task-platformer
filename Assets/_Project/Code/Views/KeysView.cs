using _Project.Code.Events;
using CodeBase.Services.EventBus;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Code.Views
{
  public class KeysView : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI KeysRemainingText;

    private IEventBusService _eventBus;

    [Inject]
    public void Construct(IEventBusService eventBus)
    {
      _eventBus = eventBus;
      Initialize();
    }

    private void Initialize()
    {
      _eventBus.Subscribe<KeysCountUpdatedEvent>(UpdateKeysCount);
    }

    private void OnDestroy()
    {
      _eventBus.Unsubscribe<KeysCountUpdatedEvent>(UpdateKeysCount);
    }

    private void UpdateKeysCount(KeysCountUpdatedEvent eventData)
    {
      KeysRemainingText.text = $"Keys: {eventData.CurrentKeys} / {eventData.TotalKeys}";
    }
  }
}