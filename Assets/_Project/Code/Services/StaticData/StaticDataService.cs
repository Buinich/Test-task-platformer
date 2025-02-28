using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private const string LevelsDataPath = "Static Data/Levels";
    private const string PlayerDataPath = "Static Data/PlayerConfig";

    private Dictionary<string, LevelStaticData> _levels;
    private PlayerConfig _playerData;

    public void Load()
    {
      _levels = Resources
        .LoadAll<LevelStaticData>(LevelsDataPath)
        .ToDictionary(x => x.LevelKey, x => x);
      
      _playerData = Resources
        .LoadAll<PlayerConfig>(PlayerDataPath)
        .FirstOrDefault();
    }
    
    public LevelStaticData ForLevel(string sceneKey) => 
      _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
        ? staticData 
        : null;

    public PlayerConfig PlayerData() => 
      _playerData;
  }
}