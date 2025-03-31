using UnityEngine;
using System.Collections;

public class Crystal : MonoBehaviour
{
    private Shooting _shootingSystem;
    [SerializeField] private float buffCooldown;
    [SerializeField] private float buffDuration;
    private bool readyToBuff;
    private bool buffEnded;
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
            timer += Time.deltaTime;
            if (!buffEnded)
            {
                if (timer >= buffDuration)
                {
                    buffEnded = true;
                    _shootingSystem.ChangeFireballDamage(-3);
                }
            }
            else
            {
                if (timer >= buffCooldown)
                {
                    readyToBuff = true;
                    timer = 0;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fireball"))
        {
            if (readyToBuff)
            {
                _shootingSystem.ChangeFireballDamage(3);
                buffEnded = false;
                readyToBuff = false;
            }
        }
    }
}
