using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject _explosion;
    private float speed;
    private int damage;
    private Camera mainCamera;
    private GameObject player;
    private Vector3 mousePosition;
    private Vector2 velocity;
    private bool paused;

    private void SetScale(float size)
    {
        transform.localScale = new Vector3(size, size, 1);
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        speed = player.GetComponent<Shooting>().GetFireballSpeed();
        damage = player.GetComponent<Shooting>().GetFireballDamage();
        SetScale(player.GetComponent<Shooting>().GetFireballSize());
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        Vector3 rotation = transform.position - mousePosition;
        float z = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z + 90);
        velocity = new Vector2(direction.x, direction.y).normalized * speed;
        _rb.linearVelocity = velocity;
        paused = false;
    }

    private void Update()
    {
        if (GameManager.pause)
        {
            paused = true;
            _rb.linearVelocity = new Vector2(0,0);
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Human>().DecreaseHealth(damage);
        }else if (collision.gameObject.CompareTag("Tutorial Enemy"))
        {
            collision.gameObject.GetComponent<TutorialHuman>().DecreaseHealth(damage);
        }
        Destroy(gameObject);
    }
}
