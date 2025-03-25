using UnityEngine;
using System.Collections;

public class Crystal : MonoBehaviour
{
    private Shooting _shootingSystem;
    [SerializeField] private float buffCooldown;
    [SerializeField] private float buffDuration;
    private bool readyToBuff;
    private float timer;

    private void Start()
    {
        _shootingSystem = GameObject.FindWithTag("Player").GetComponent<Shooting>();
        readyToBuff = true;
        timer = 0;
    }

    private void Update()
    {
        if (!readyToBuff && !GameManager.pause)
        {
            if (timer >= buffCooldown)
            {
                readyToBuff = true;
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
        if (collision.gameObject.CompareTag("Fireball"))
        {
            StartCoroutine("Buff");
            readyToBuff = false;
        }
    }

    private IEnumerator Buff()
    {
        _shootingSystem.ChangeFireballDamage(2);
        yield return new WaitForSeconds(buffDuration);
        _shootingSystem.ChangeFireballDamage(-2);
    }
}
