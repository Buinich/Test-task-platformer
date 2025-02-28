using System;
using UnityEngine;

namespace CodeBase.StaticData
{
  [Serializable]
  public class KeySpawnerStaticData
  {
    public Vector3 Position;

    public KeySpawnerStaticData(Vector3 position)
    {
      Position = position;
    }
  }
}