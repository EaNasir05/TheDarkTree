using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    private float speed;
    private int damage;
    private Camera mainCamera;
    private GameObject player;
    private Vector3 mousePosition;

    private void SetScale(float size)
    {
        transform.localScale = new Vector3(size, size, 1);
    }

    private void Start()
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
        _rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("NEMICO COLPITO");
        }
        Destroy(gameObject);
    }
}
