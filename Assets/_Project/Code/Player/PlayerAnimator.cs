using _Project.Code.Events;
using CodeBase.Services.EventBus;
using UnityEngine;
using Zenject;

namespace _Project.Code.Player
{
  public class PlayerAnimator : MonoBehaviour
  {
    [SerializeField] private Animator Animator;
    
    private static readonly int MoveSpeedHash = Animator.StringToHash("Speed");
    private static readonly int VerticalSpeedHash = Animator.StringToHash("VerticalSpeed");
    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int DieHash = Animator.StringToHash("Die");
    
    private IEventBusService _eventBus;
    private Rigidbody2D _rb;

    [Inject]
    public void Construct(IEventBusService eventBus)
    {
      _eventBus = eventBus;
      Initialize();
    }

    private void Initialize()
    {
      _rb = GetComponent<Rigidbody2D>();
      
      _eventBus.Subscribe<DamageTakenEvent>(PlayHitAnimation);
      _eventBus.Subscribe<PlayerDiedEvent>(PlayDeathAnimation);
    }

    private void OnDestroy()
    {
      _eventBus.Unsubscribe<DamageTakenEvent>(PlayHitAnimation);
      _eventBus.Unsubscribe<PlayerDiedEvent>(PlayDeathAnimation);
    }

    private void Update()
    {
      UpdateMovementAnimation();
      UpdateVerticalAnimation();
      UpdateFacingDirection();
    }

    private void UpdateMovementAnimation()
    {
      float horizontalSpeed = Mathf.Abs(_rb.linearVelocity.x);
      Animator.SetFloat(MoveSpeedHash, horizontalSpeed);
    }

    private void UpdateVerticalAnimation()
    {
      float verticalSpeed = _rb.linearVelocity.y;
      Animator.SetFloat(VerticalSpeedHash, verticalSpeed);
    }

    private void UpdateFacingDirection()
    {
      float horizontalSpeed = _rb.linearVelocity.x;
      
      if (Mathf.Abs(horizontalSpeed) > 0.1f) 
        transform.localScale = new Vector3(
          Mathf.Sign(horizontalSpeed)
          , 1
          , 1);
    }
    
    private void PlayHitAnimation(DamageTakenEvent evt)
    {
      Animator.SetTrigger(HitHash);
    }

    private void PlayDeathAnimation(PlayerDiedEvent evt)
    {
      Animator.SetTrigger(DieHash);
    }
  }
}