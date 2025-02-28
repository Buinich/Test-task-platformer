using _Project.Code.Events;
using CodeBase.Services.EventBus;
using UnityEngine;
using Zenject;

namespace _Project.Code.Level.Traps
{
  public class Chasm : MonoBehaviour
  {
    private IEventBusService _eventBus;

    [Inject]
    public void Construct(IEventBusService eventBus)
    {
      _eventBus = eventBus;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.CompareTag("Player"))
        KillPlayer();
    }

    private void KillPlayer()
    {
      _eventBus.Publish(new DamageTakenEvent(int.MaxValue));
    }
  }
}