using UnityEngine;
using System.Collections;

public class Bulb : MonoBehaviour
{
    [SerializeField] private Collider2D collider2d;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private float explosionCooldown;
    private bool readyToExplode = true;
    private float timer = 0;

    private void Update()
    {
        if (!readyToExplode && !GameManager.pause)
        {
            if (timer >= explosionCooldown)
            {
                readyToExplode = true;
                collider2d.enabled = true;
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fireball") )
        {
            StartCoroutine("CreateExplosion");
            collider2d.enabled = false;
            readyToExplode = false;
        }
    }

    private IEnumerator CreateExplosion()
    {
        GameObject explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
        yield return new WaitForSeconds((float)0.25);
        Destroy(explosion);
    }
}
