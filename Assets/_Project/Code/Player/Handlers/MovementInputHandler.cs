using CodeBase.Services.Input;
using UnityEngine;

namespace _Project.Code.Player.Handlers
{
  public class MovementInputHandler
  {
    private readonly IInputService _inputService;

    public MovementInputHandler(IInputService inputService)
    {
      _inputService = inputService;
    }

    public float HorizontalInput => GetHorizontalInput();
    public bool IsJumpPressed => IsJumpButtonPressed();
    public bool IsJumpHeld => IsJumpButtonHeld();

    private float GetHorizontalInput() =>
      _inputService.Axis.x;

    private bool IsJumpButtonPressed() =>
      _inputService.IsJumpButtonPressed;

    private bool IsJumpButtonHeld() =>
      _inputService.IsJumpButtonHold;
  }
}