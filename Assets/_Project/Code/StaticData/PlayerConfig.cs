using UnityEngine;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "new PlayerConfig", menuName = "Static Data/PlayerConfig")]
  public class PlayerConfig : ScriptableObject
  {
    public float MoveSpeed;
    public float JumpForce;
    public float CoyoteTime;
    public float JumpBufferTime;
    public float GroundCheckRadius;
    public float TimeToMaxJump;
    public float MaxHealth;
    public LayerMask GroundLayer;
  }
}