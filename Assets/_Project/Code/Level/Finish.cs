using _Project.Code.Events;
using _Project.Code.Infrastructure.SceneManagement;
using CodeBase.Services.EventBus;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Project.Code.Level
{
  public class Finish : MonoBehaviour
  {
    private const string SceneName = "Main";
    
    private IEventBusService _eventBus;
    private bool _isActivated;

    [Inject]
    public void Construct(IEventBusService eventBus, ISceneLoader sceneLoader)
    {
      _eventBus = eventBus;

      Initialize();
    }

    private void Initialize()
    {
      _eventBus.Subscribe<AllKeysCollectedEvent>(ActivateDoor);
      gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
      _eventBus.Unsubscribe<AllKeysCollectedEvent>(ActivateDoor);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.CompareTag("Player") && _isActivated)
      {
        other.gameObject.SetActive(false);
        RestartLevel();
      }
    }

    private void ActivateDoor(AllKeysCollectedEvent _)
    {
      _isActivated = true;
      gameObject.SetActive(true);
    }

    private async void RestartLevel()
    {
      _eventBus.Clear();
      await SceneManager.LoadSceneAsync(SceneName);
    }
  }
}