using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float fireballCooldown;
    [SerializeField] private int fireballDamage;
    [SerializeField] private float fireballSpeed;
    [SerializeField] private float fireballSize;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Transform _fireballSpawnPoint;
    [SerializeField] private GameObject _fireBall;
    private InputAction _moveAction;
    private bool readyToFire;

    private void Start()
    {
        _moveAction = _playerInput.actions["Move"];
        readyToFire = true;
    }

    private void FixedUpdate()
    {
        Move(_moveAction.ReadValue<Vector2>());
        if (Input.GetMouseButton(0))
        {
            StartCoroutine("Shoot");
        }
    }

    public int GetFireballDamage()
    {
        return fireballDamage;
    }

    public float GetFireballSpeed()
    {
        return fireballSpeed;
    }

    public float GetFireballSize()
    {
        return fireballSize;
    }

    private void Move(Vector2 movement)
    {
        _rb.linearVelocity = movement * movementSpeed;
    }

    private IEnumerator Shoot()
    {
        if (readyToFire)
        {
            readyToFire = false;
            Instantiate(_fireBall, _fireballSpawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(fireballCooldown);
            readyToFire = true;
        }
    }
}
