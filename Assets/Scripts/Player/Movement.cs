using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float movementSpeed;
    private InputAction _moveAction;

    private void Start()
    {
        _moveAction = _playerInput.actions["Move"];
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _moveAction.ReadValue<Vector2>() * movementSpeed;
    }
}
