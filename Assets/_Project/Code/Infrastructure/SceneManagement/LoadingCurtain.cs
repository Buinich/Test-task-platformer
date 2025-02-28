using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.Infrastructure.SceneManagement
{
  public class LoadingCurtain : MonoBehaviour
  {
    [SerializeField] private CanvasGroup loadingCanvas;
    [SerializeField] private bool useLoadingBar;
    [HideInInspector] public Image loadingBar;

    public bool UseLoadingBar => useLoadingBar;

    private AsyncOperation _localLoadOperation;

    public AsyncOperation LocalLoadOperation
    {
      set
      {
        _isLoading = true;
        ResetBar();
        _localLoadOperation = value;
      }
    }

    private float _targetProgress;
    private bool _isLoading;

    private void Update()
    {
      if (!useLoadingBar)
        return;

      if (!_isLoading || _localLoadOperation == null)
        return;

      if (_localLoadOperation.isDone || loadingBar.fillAmount >= _targetProgress)
      {
        _isLoading = false;
        loadingBar.fillAmount = _targetProgress;
      }
      else
      {
        float currentProgress = loadingBar.fillAmount;

        loadingBar.fillAmount = Mathf.Lerp(currentProgress, _localLoadOperation.progress, Time.deltaTime);
      }
    }

    public void ResetBar()
    {
      if (!useLoadingBar)
        return;

      loadingBar.fillAmount = 0;
      _targetProgress = 1f;
    }

    public async void Show(float fadeTime = 0)
    {
      if (fadeTime <= 0)
      {
        gameObject.SetActive(true);
        return;
      }

      await Enable(true, fadeTime);
    }

    public async void Hide(float fadeTime = 0)
    {
      if (fadeTime <= 0)
      {
        gameObject.SetActive(false);
        return;
      }

      await Enable(false, fadeTime);
    }

    private async Task Enable(bool enable, float fadeTime)
    {
      float timeElapsed = 0f;

      float start = loadingCanvas.alpha;
      float end = enable ? 1f : 0f;

      while (timeElapsed < fadeTime)
      {
        timeElapsed += Time.deltaTime;
        float t = timeElapsed / fadeTime;

        loadingCanvas.alpha = Mathf.Lerp(start, end, t);

        await Task.Yield();
      }

      loadingCanvas.alpha = end;
    }
  }
}