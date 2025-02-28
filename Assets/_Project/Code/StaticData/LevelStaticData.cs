using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
  public class LevelStaticData : ScriptableObject
  {
    public string LevelKey;
    public List<Vector3> KeySpawners;
    public List<Vector3> HealthPackSpawners;
    public Vector3 PlayerSpawner;
    public float HealthPackHeal = 30f;
    public float SpikeDamage = 20f;
  }
}