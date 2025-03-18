using UnityEngine;
using UnityEngine.InputSystem;

public class PC : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _fireballDamage;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Transform _fireballSpawnPoint;
    private InputAction _moveAction;

    private void Start()
    {
        _moveAction = _playerInput.actions["Move"];
    }

    private void FixedUpdate()
    {
        Move(_moveAction.ReadValue<Vector2>());
    }

    private void Move(Vector2 movement)
    {
        _rb.linearVelocity = movement * _movementSpeed;
    }

    private void Shoot()
    {
        
    }
}
