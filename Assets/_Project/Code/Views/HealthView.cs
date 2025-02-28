using _Project.Code.Events;
using CodeBase.Services.EventBus;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Code.Views
{
  public class HealthView : MonoBehaviour
  {
    [SerializeField] private Image HealthBar;

    private IEventBusService _eventBus;

    [Inject]
    public void Construct(IEventBusService eventBus)
    {
      _eventBus = eventBus;
      Initialize();
    }

    private void Initialize()
    {
      _eventBus.Subscribe<HealthChangedEvent>(UpdateHealthBar);
    }

    private void OnDestroy()
    {
      _eventBus.Unsubscribe<HealthChangedEvent>(UpdateHealthBar);
    }

    private void UpdateHealthBar(HealthChangedEvent eventData)
    {
      float percentage = eventData.CurrentHealth / eventData.MaxHealth;
      HealthBar.fillAmount = percentage;
    }
  }
}