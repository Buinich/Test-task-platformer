using _Project.Code.Events;
using CodeBase.Services.EventBus;
using CodeBase.Services.StaticData;
using UnityEngine;
using Zenject;

namespace _Project.Code.Level.Traps
{
  public class Spike : MonoBehaviour
  {
    private IEventBusService _eventBus;
    private float _damage;

    [Inject]
    public void Construct(IEventBusService eventBus, IStaticDataService staticData)
    {
      _eventBus = eventBus;
      
      _damage = staticData.ForLevel("Main").SpikeDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.CompareTag("Player"))
        DealDamage();
    }

    private void DealDamage()
    {
      _eventBus.Publish(new DamageTakenEvent(_damage));
    }
  }
}