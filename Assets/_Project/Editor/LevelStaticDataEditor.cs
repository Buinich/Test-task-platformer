using System.Linq;
using _Project.Code.Level.Loot;
using _Project.Code.Player;
using CodeBase.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Editor
{
  [CustomEditor(typeof(LevelStaticData))]
  public class LevelStaticDataEditor : UnityEditor.Editor
  {
    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      LevelStaticData levelData = (LevelStaticData)target;

      if (GUILayout.Button("Collect"))
      {
        levelData.KeySpawners = FindObjectsOfType<KeySpawnMarker>()
          .Select(x => x.transform.position)
          .ToList();

        levelData.HealthPackSpawners = FindObjectsOfType<HealthPackSpawnMarker>()
          .Select(x => x.transform.position)
          .ToList();

        levelData.PlayerSpawner = FindObjectOfType<PlayerSpawnMarker>()
          .transform.position;

        levelData.LevelKey = SceneManager.GetActiveScene().name;
      }

      EditorUtility.SetDirty(target);
    }
  }
}