using System;
using _Project.Code.Player.Handlers;
using CodeBase.Services.Input;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using UnityEngine;
using Zenject;

namespace _Project.Code.Player
{
  public class PlayerController : MonoBehaviour
  {
    public bool IsGrounded => _groundChecker.IsGrounded;

    [SerializeField] private Transform GroundCheck;

    private Rigidbody2D _rb;
    private MovementInputHandler _inputHandler;
    private GroundCheckHandler _groundChecker;
    private JumpHandler _jumpHandler;
    private PlayerConfig _config;

    [Inject]
    private void Construct(IStaticDataService staticData, IInputService inputService)
    {
      _config = staticData.PlayerData();

      _rb = GetComponent<Rigidbody2D>();
      _inputHandler = new MovementInputHandler(inputService);
      _groundChecker = new GroundCheckHandler(GroundCheck, _config.GroundCheckRadius, _config.GroundLayer);
      _jumpHandler = new JumpHandler(_rb, _config.JumpForce, _config.CoyoteTime, _config.JumpBufferTime,
        _config.TimeToMaxJump);
    }

    private void Update()
    {
      _groundChecker.Update();
      _jumpHandler.Update(_groundChecker.IsGrounded, _inputHandler.IsJumpPressed, _inputHandler.IsJumpHeld);

      HandleWallCollision();
    }

    private void FixedUpdate()
    {
      Move(_inputHandler.HorizontalInput);
    }

    public void Move(float horizontalInput)
    {
      Vector2 velocity = _rb.linearVelocity;

      velocity.x = CalculateHorizontalMovement(horizontalInput);
      _rb.linearVelocity = velocity;
    }

    private float CalculateHorizontalMovement(float horizontalInput) =>
      !IsCollidingWithWall(horizontalInput)
        ? horizontalInput * _config.MoveSpeed
        : 0;

    private bool IsCollidingWithWall(float horizontalInput)
    {
      Vector2 direction = new Vector2(horizontalInput, 0).normalized;
      Vector2[] rayOrigins = GetRayOrigins();

      return CheckWallCollision(rayOrigins, direction);
    }

    private Vector2[] GetRayOrigins()
    {
      float halfHeight = transform.localScale.y * 0.5f;
      Vector2 center = _rb.position;

      return new[]
      {
        center,
        center + Vector2.up * (halfHeight * 0.9f),
        center - Vector2.up * (halfHeight * 0.9f)
      };
    }

    private bool CheckWallCollision(Vector2[] rayOrigins, Vector2 direction)
    {
      const float rayLength = 0.5f;

      foreach (Vector2 origin in rayOrigins)
      {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayLength, _config.GroundLayer);

#if UNITY_EDITOR
        DrawCollisionRay(origin, direction, rayLength, hit.collider != null);
#endif

        if (hit.collider != null)
          return true;
      }

      return false;
    }

    private void HandleWallCollision()
    {
      if (ShouldSlideAlongWall())
        _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
    }

    private bool ShouldSlideAlongWall() =>
      !_groundChecker.IsGrounded
      && IsCollidingWithWall(_inputHandler.HorizontalInput);

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
      if (GroundCheck != null)
      {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheck.position, _config?.GroundCheckRadius ?? .3f);
      }
    }

    private void DrawCollisionRay(Vector2 origin, Vector2 direction, float length, bool hasHit)
    {
      Color rayColor = hasHit ? Color.red : Color.green;
      Debug.DrawRay(origin, direction * length, rayColor, 0.1f);
    }
#endif
  }
}