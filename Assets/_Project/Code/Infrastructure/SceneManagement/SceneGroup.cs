using System;
using System.Collections.Generic;
using System.Linq;
using Eflatun.SceneReference;

namespace _Project.Code.Infrastructure.SceneManagement
{
  public enum SceneType
  {
    ActiveScene = 0,
    Bootstrap = 1,
    HUD = 2
  }

  [Serializable]
  public class SceneData
  {
    public SceneReference sceneReference;
    public string Name => sceneReference.Name;
    public SceneType sceneType;
  }

  [Serializable]
  public class SceneGroup
  {
    public string groupName = "New Scene Group";
    public List<SceneData> scenes;

    public string FindSceneNameByType(SceneType sceneType) =>
      scenes.FirstOrDefault(s => s.sceneType == sceneType)?.Name;
  }
}