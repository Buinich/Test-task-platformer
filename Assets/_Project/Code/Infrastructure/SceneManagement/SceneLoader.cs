using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _Project.Code.Infrastructure.SceneManagement
{
  public class SceneLoader : MonoBehaviour, ISceneLoader
  {
    public event Action<string> OnSceneLoaded = delegate { };
    public event Action<string> OnSceneUnloaded = delegate { };
    public event Action<string> OnSceneGroupLoaded = delegate { };

    [SerializeField] private LoadingCurtain LoadingCurtain;
    [SerializeField] private UnityEngine.Camera LoadingCamera;
    [SerializeField] private SceneGroup[] SceneGroups;

    private string _bootScene;
    private SceneGroup _activeSceneGroup;
    private string _activeScene;
    private bool _isInitialized;

    private void Awake()
    {
      _bootScene = SceneManager.GetActiveScene().name;
    }

    private void Start()
    {
      OnSceneLoaded += sceneName => Debug.Log($"Loaded scene {sceneName}");
      OnSceneUnloaded += sceneName => Debug.Log($"Unloaded scene {sceneName}");
      OnSceneGroupLoaded += groupName => Debug.Log($"Loaded scene group {groupName}");

      HideLoading();
    }

    public async Task LoadSceneGroupAsync(int index, Action onLoaded = null)
    {
      if (index < 0 || index >= SceneGroups.Length)
      {
#if UNITY_EDITOR
        Debug.LogError($"Scene Group index {index} is out of range.");
#endif
        return;
      }

      await LoadSceneGroupAsync(SceneGroups[index]);

      onLoaded?.Invoke();
    }

    public async Task LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Additive,
      bool setActive = true, Action onLoaded = null)
    {
      AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);

      if (operation == null)
        return;

      ShowLoading();
      LoadingCurtain.LocalLoadOperation = operation;
      if (loadSceneMode == LoadSceneMode.Single)
      {
        LoadingCurtain.ResetBar();
      }

      while (!operation.isDone)
      {
        await Task.Delay(100);
      }

      HideLoading();

      if (!setActive)
      {
        OnSceneLoaded?.Invoke(sceneName);
        onLoaded?.Invoke();

        return;
      }

      Scene loadedScene = SceneManager.GetSceneByName(sceneName);
      if (loadedScene.IsValid())
        SceneManager.SetActiveScene(loadedScene);

      OnSceneLoaded?.Invoke(sceneName);
      onLoaded?.Invoke();
    }

    public async Task UnloadSceneAsync(string sceneName)
    {
      if (!SceneManager.GetSceneByName(sceneName).IsValid())
        return;

      AsyncOperation operation = SceneManager.UnloadSceneAsync(sceneName);

      if (operation == null)
        return;

      while (!operation.isDone)
      {
        await Task.Yield();
      }
    }

    private async Task LoadSceneGroupAsync(SceneGroup sceneGroup, bool reloadDuplicateScenes = false)
    {
      LoadingCurtain.ResetBar();

      _activeSceneGroup = sceneGroup;
      List<string> loadedScenes = new();

      if (!string.IsNullOrWhiteSpace(_bootScene)) 
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_bootScene));

      await UnloadScenesAsync();

      int scenesCount = SceneManager.sceneCount;

      for (int i = 0; i < scenesCount; i++)
        loadedScenes.Add(SceneManager.GetSceneAt(i).name);

      int scenesToLoad = _activeSceneGroup.scenes.Count;

      for (int i = 0; i < scenesToLoad; i++)
      {
        SceneData sceneData = sceneGroup.scenes[i];

        if (reloadDuplicateScenes is false && loadedScenes.Contains(sceneData.Name))
          continue;

        await LoadSceneAsync(sceneData.Name, setActive: false);
      }

      Scene activeScene =
        SceneManager.GetSceneByName(_activeSceneGroup.FindSceneNameByType(SceneType.ActiveScene));

      if (activeScene.IsValid())
      {
        if (_activeScene != null && !_activeScene.Equals(activeScene.name))
          await UnloadSceneAsync(_activeScene);

        SceneManager.SetActiveScene(activeScene);
        
        _activeScene = activeScene.name;
      }

      OnSceneGroupLoaded?.Invoke(_activeSceneGroup.groupName);
    }

    private async Task UnloadScenesAsync()
    {
      List<string> scenes = new();

      string activeScene = SceneManager.GetActiveScene().name;

      int sceneCount = SceneManager.sceneCount;

      for (int i = sceneCount - 1; i > 0; i--)
      {
        Scene sceneAt = SceneManager.GetSceneAt(i);
        if (!sceneAt.isLoaded)
          continue;

        string sceneName = sceneAt.name;
        if (sceneName.Equals(activeScene))
          continue;

        scenes.Add(sceneName);
      }

      AsyncOperationGroup operationGroup = new(scenes.Count);

      foreach (string scene in scenes)
      {
        AsyncOperation operation = SceneManager.UnloadSceneAsync(scene);
        if (operation == null)
          continue;

        operationGroup.Operations.Add(operation);

        OnSceneUnloaded?.Invoke(scene);
      }

      while (!operationGroup.IsDone)
      {
        await Task.Delay(100);
      }
    }

    private void ShowLoading()
    {
      LoadingCamera.gameObject.SetActive(true);
      LoadingCurtain.Show(fadeTime: 1);
    }

    private void HideLoading()
    {
      LoadingCamera.gameObject.SetActive(false);
      LoadingCurtain.Hide(fadeTime: 1);
    }
  }
}