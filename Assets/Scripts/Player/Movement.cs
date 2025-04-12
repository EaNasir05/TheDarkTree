using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float movementSpeed;
    private InputAction _moveAction;

    public void ChangeMovementSpeed(float value) { movementSpeed += value; }

    private void Start()
    {
        _moveAction = _playerInput.actions["Move"];
    }

    private void Update()
    {
        //cambia direzione sprite PG se si muove lateralmente
        if (GameManager.pause || GameManager.selectingTrap)
        {
            _rb.linearVelocity = new Vector2(0,0);
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.pause && !GameManager.selectingTrap)
        {
            _rb.linearVelocity = _moveAction.ReadValue<Vector2>() * movementSpeed;
        }
    }
}
