using UnityEngine;

namespace _Project.Code.Player.Handlers
{
  public class JumpHandler
  {
    private readonly Rigidbody2D _rb;
    private readonly float _jumpForce;
    private readonly float _coyoteTime;
    private readonly float _jumpBufferTime;
    private readonly float _timeToMaxJump;

    private float _coyoteTimeCounter;
    private float _jumpBufferCounter;
    private float _jumpHoldTime;
    private bool _isJumping;

    public JumpHandler(Rigidbody2D rigidbody, float force, float coyoteDuration, float bufferDuration,
      float timeToMaxJump)
    {
      _rb = rigidbody;
      _jumpForce = force;
      _coyoteTime = coyoteDuration;
      _jumpBufferTime = bufferDuration;
      _timeToMaxJump = timeToMaxJump;
    }

    public void Update(bool isGrounded, bool jumpPressed, bool jumpHeld)
    {
      UpdateCoyoteTime(isGrounded);
      UpdateJumpBuffer(jumpPressed);

      if (_isJumping)
      {
        if (jumpHeld)
          _jumpHoldTime += Time.deltaTime;
        else
          InterruptJump();
      }

      if (CanJump())
      {
        PerformJump();
      }
    }

    private void UpdateCoyoteTime(bool isGrounded)
    {
      _coyoteTimeCounter = isGrounded ? _coyoteTime : Mathf.Max(_coyoteTimeCounter - Time.deltaTime, 0);
    }

    private void UpdateJumpBuffer(bool jumpPressed)
    {
      _jumpBufferCounter = jumpPressed
        ? _jumpBufferTime
        : Mathf.Max(_jumpBufferCounter - Time.deltaTime, 0);
    }

    private bool CanJump() =>
      _coyoteTimeCounter > 0
      && _jumpBufferCounter > 0;

    private void PerformJump()
    {
      _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
      _coyoteTimeCounter = 0;
      _jumpBufferCounter = 0;
      _jumpHoldTime = 0;
      _isJumping = true;
    }

    private void InterruptJump()
    {
      if (!_isJumping)
        return;

      float progress = Mathf.Clamp01(_jumpHoldTime / _timeToMaxJump);

      if (_rb.linearVelocity.y < 0)
        return;

      float reducedVelocity = _jumpForce * progress * 0.5f;

      _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, Mathf.Min(_rb.linearVelocity.y, reducedVelocity));
      _isJumping = false;
    }
  }
}