using _Project.Code.Events;

namespace _Project.Code.Controllers
{
  public interface IHealthController
  {
    void Initialize();
    void OnDamageTaken(DamageTakenEvent evt);
    void OnHealthRestored(HealthRestoredEvent eventData);
    void Cleanup();
  }
}