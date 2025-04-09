using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    private Transform _target;
    private Vector2 velocity;
    private bool paused;

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Tree").transform;
        Vector3 direction = _target.position - transform.position;
        Vector3 rotation = transform.position - _target.position;
        float z = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z + 90);
        velocity = new Vector2(direction.x, direction.y).normalized * 10;
        _rb.linearVelocity = velocity;
        paused = false;
    }

    private void Update()
    {
        if (GameManager.pause)
        {
            paused = true;
            _rb.linearVelocity = new Vector2(0, 0);
        }
        if (paused)
        {
            if (!GameManager.pause)
            {
                _rb.linearVelocity = velocity;
                paused = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            GameManager.instance.DecreaseHealthPoints(1);
        }
        Destroy(gameObject);
    }
}
