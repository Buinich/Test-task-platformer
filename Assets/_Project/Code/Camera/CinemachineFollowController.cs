using _Project.Code.Events;
using CodeBase.Services.EventBus;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace _Project.Code.Camera
{
  public class CinemachineCameraController : MonoBehaviour
  {
    [SerializeField] private CinemachineCamera VirtualCamera;

    private IEventBusService _eventBus;

    [Inject]
    public void Construct(IEventBusService eventBus)
    {
      _eventBus = eventBus;
      Initialize();
    }

    private void Initialize()
    {
      _eventBus.Subscribe<PlayerSpawnedEvent>(SetCameraTarget);
    }

    private void OnDestroy()
    {
      _eventBus.Unsubscribe<PlayerSpawnedEvent>(SetCameraTarget);
    }

    private void SetCameraTarget(PlayerSpawnedEvent eventData)
    {
      VirtualCamera.Target.TrackingTarget = eventData.Player.transform;
    }
  }
}