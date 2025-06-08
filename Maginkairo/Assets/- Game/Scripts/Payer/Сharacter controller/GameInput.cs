using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private PlayerInputActions controls;

    public event Action OnActionPressed;
    public event Action OnPausePressed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        controls = new PlayerInputActions();

        controls.Player.Action.started += OnActionPerformed;
        controls.Player.Pause.performed += OnPausePerformed;

        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        if (controls == null) return;
        controls.Player.Action.started -= OnActionPerformed;
        controls.Player.Pause.performed -= OnPausePerformed;
        controls.Player.Disable();
    }

    private void OnActionPerformed(InputAction.CallbackContext ctx) => OnActionPressed?.Invoke();
    private void OnPausePerformed(InputAction.CallbackContext ctx) => OnPausePressed?.Invoke();

    public Vector2 GetMoveVector()
    {
        Vector2 v = controls.Player.Move.ReadValue<Vector2>();
        if (v.sqrMagnitude > 1f) v.Normalize();
        return v;
    }
}
