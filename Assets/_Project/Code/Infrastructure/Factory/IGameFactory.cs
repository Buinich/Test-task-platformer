using _Project.Code.Level.Loot;
using _Project.Code.Player;
using CodeBase.Services;
using UnityEngine;

namespace _Project.Code.Infrastructure.Factory
{
  public interface IGameFactory
  {
    PlayerController CreatePlayer(Vector3 at);
    LootPiece CreateLoot(Vector3 at, Transform parent);
    LootPiece CreateHealth(Vector3 at, Transform parent);
  }
}