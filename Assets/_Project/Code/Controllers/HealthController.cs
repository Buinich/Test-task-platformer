using _Project.Code.Events;
using CodeBase.Data;
using CodeBase.Services.EventBus;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using UnityEngine;

namespace _Project.Code.Controllers
{
  public class HealthController : IHealthController
  {
    private readonly IStaticDataService _staticData;
    private readonly IEventBusService _eventBus;
    private HealthModel _healthModel;

    public HealthController(IStaticDataService staticDataService, IEventBusService eventBus)
    {
      _staticData = staticDataService;
      _eventBus = eventBus;
      
      Initialize();
    }

    public void Initialize()
    {
      PlayerConfig config = _staticData.PlayerData();
      
      _healthModel = new HealthModel(config);
      
      _healthModel.OnHealthChanged += PublishHealthChanged;
      _healthModel.OnPlayerDied += PublishPlayerDied;
      _eventBus.Subscribe<DamageTakenEvent>(OnDamageTaken);
      _eventBus.Subscribe<HealthRestoredEvent>(OnHealthRestored);
      _eventBus.Subscribe<HealthResetEvent>(OnHealthReset);
      PublishHealthChanged(_healthModel.CurrentHealth, _healthModel.MaxHealth);
    }

    public void Cleanup()
    {
      if (_healthModel != null)
        _healthModel.OnHealthChanged -= PublishHealthChanged;

      _eventBus.Unsubscribe<DamageTakenEvent>(OnDamageTaken);
      _eventBus.Unsubscribe<HealthRestoredEvent>(OnHealthRestored);
    }

    public void OnDamageTaken(DamageTakenEvent eventData)
    {
      _healthModel?.TakeDamage(eventData.Amount);
    }

    public void OnHealthRestored(HealthRestoredEvent eventData)
    {
      _healthModel?.RestoreHealth(eventData.Amount);
    }

    private void OnHealthReset(HealthResetEvent _)
    {
      _healthModel?.ResetHealth();
    }

    private void PublishHealthChanged(float currentHealth, float maxHealth)
    {
      _eventBus.Publish(new HealthChangedEvent(currentHealth, maxHealth));
    }
    
    private void PublishPlayerDied()
    {
      _eventBus.Publish(new PlayerDiedEvent());
    }
  }
}