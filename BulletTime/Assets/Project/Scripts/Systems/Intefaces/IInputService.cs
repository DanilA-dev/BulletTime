using System;
using UnityEngine;

public interface IInputService
{
    public event Action<Vector2> PointerPosition;
    public event Action<Vector2> PointerDelta;
    public event Action PointerPerformed;
    public event Action PointerHold;
    public event Action PointerCanceled;
    public event Action PauseButton;
}
