using UnityEngine;

namespace CodeBase.Services.Input
{
  public class InputService : IInputService
  {
    private readonly InputActions _inputActions;

    public InputService()
    {
      _inputActions = new InputActions();
      _inputActions.Gameplay.Enable();
    }

    public Vector2 Axis => GetMoveAxis();
    public bool IsJumpButtonPressed => IsJumpPressed();
    public bool IsJumpButtonHold => IsJumpHeld();

    private Vector2 GetMoveAxis()
    {
      return _inputActions.Gameplay.Move.ReadValue<Vector2>();
    }

    private bool IsJumpPressed()
    {
      return _inputActions.Gameplay.Jump.WasPressedThisFrame();
    }

    private bool IsJumpHeld()
    {
      return _inputActions.Gameplay.Jump.IsPressed();
    }
  }
}