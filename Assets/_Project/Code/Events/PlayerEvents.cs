using UnityEngine;

namespace _Project.Code.Events
{
  public class PlayerDiedEvent { }
  
  public class PlayerSpawnedEvent
  {
    public GameObject Player { get; }

    public PlayerSpawnedEvent(GameObject player)
    {
      Player = player;
    }
  }
}