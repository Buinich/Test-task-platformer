using System;
using System.Collections.Generic;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Data
{
  public class KeysModel
  {
    public event Action<int, int> OnKeysCountChanged;

    private readonly List<Vector3> _keySpawners;
    private int _keysCollected;

    public int TotalKeys => _keySpawners.Count;
    public int KeysCollected => _keysCollected;

    public KeysModel(LevelStaticData levelData)
    {
      _keySpawners = new List<Vector3>(levelData.KeySpawners);
      _keysCollected = 0;
    }

    public void CollectKey()
    {
      _keysCollected++;
      OnKeysCountChanged?.Invoke(KeysCollected, TotalKeys);
    }

    public void ResetKeys()
    {
      _keysCollected = 0;
      OnKeysCountChanged?.Invoke(KeysCollected, TotalKeys);
    }
  }
}