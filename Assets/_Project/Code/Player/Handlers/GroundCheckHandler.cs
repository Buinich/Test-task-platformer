using UnityEngine;

namespace _Project.Code.Player.Handlers
{
  public class GroundCheckHandler
  {
    private readonly Transform _groundCheck;
    private readonly float _radius;
    private readonly LayerMask _groundLayer;

    public bool IsGrounded { get; private set; }

    public GroundCheckHandler(Transform checkTransform, float checkRadius, LayerMask layer)
    {
      _groundCheck = checkTransform;
      _radius = checkRadius;
      _groundLayer = layer;
    }

    public void Update()
    {
      IsGrounded = Physics2D.OverlapCircle(_groundCheck.position, _radius, _groundLayer);
    }
  }
}