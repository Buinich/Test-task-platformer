using _Project.Code.Events;

namespace _Project.Code.Controllers
{
  public interface IKeysController
  {
    void Initialize(string levelName);
    void Cleanup();
    void OnKeyCollected(KeyCollectedEvent _);
  }
}