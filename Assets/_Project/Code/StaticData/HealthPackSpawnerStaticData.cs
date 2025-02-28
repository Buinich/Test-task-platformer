using System;
using UnityEngine;

namespace CodeBase.StaticData
{
  [Serializable]
  public class HealthPackSpawnerStaticData
  {
    public Vector3 Position;

    public HealthPackSpawnerStaticData(Vector3 position)
    {
      Position = position;
    }
  }
}