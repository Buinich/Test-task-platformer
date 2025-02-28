using _Project.Code.Infrastructure.AssetManagement;
using _Project.Code.Level.Loot;
using _Project.Code.Player;
using CodeBase.Services.StaticData;
using UnityEngine;
using Zenject;

namespace _Project.Code.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider _assets;
    private readonly IStaticDataService _staticData;
    private readonly DiContainer _container;

    public GameFactory(IAssetProvider assets, IStaticDataService staticData, DiContainer container)
    {
      _assets = assets;
      _staticData = staticData;
      _container = container;
    }

    public PlayerController CreatePlayer(Vector3 at)
    {
      GameObject playerObject = _assets.InstantiateWithInject(AssetPath.PlayerPath, at);
      
      return playerObject.GetComponent<PlayerController>();
    }

    public LootPiece CreateLoot(Vector3 at, Transform parent)
    {
      LootPiece lootPiece = _assets.Instantiate(AssetPath.KeyPath, at: at, parent)
        .GetComponent<LootPiece>();

      return lootPiece;
    }
    
    public LootPiece CreateHealth(Vector3 at, Transform parent)
    {
      LootPiece lootPiece = _assets.Instantiate(AssetPath.HealthPackPath, at: at, parent)
        .GetComponent<LootPiece>();

      return lootPiece;
    }
  }
}