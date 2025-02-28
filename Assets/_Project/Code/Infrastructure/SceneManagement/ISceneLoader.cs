using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _Project.Code.Infrastructure.SceneManagement
{
  public interface ISceneLoader
  {
    event Action<string> OnSceneLoaded;
    event Action<string> OnSceneUnloaded;
    event Action<string> OnSceneGroupLoaded;
    Task LoadSceneGroupAsync(int index, Action onLoaded = null);

    Task LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Additive,
      bool setActive = true, Action onLoaded = null);

    Task UnloadSceneAsync(string sceneName);
  }
}