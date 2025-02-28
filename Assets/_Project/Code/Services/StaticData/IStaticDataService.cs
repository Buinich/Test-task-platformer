using CodeBase.StaticData;

namespace CodeBase.Services.StaticData
{
  public interface IStaticDataService
  {
    void Load();
    LevelStaticData ForLevel(string sceneKey);
    PlayerConfig PlayerData();
  }
}