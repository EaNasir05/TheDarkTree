using UnityEngine;
using System.Collections;

public class Crystal : MonoBehaviour
{
    private Shooting _shootingSystem;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private float buffCooldown;
    [SerializeField] private float buffDuration;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private AudioClip _activationAudio;
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
                    _shootingSystem.ChangeFireballSpeed(-5);
                }
            }
            else
            {
                if (timer >= buffCooldown)
                {
                    _collider.enabled = true;
                    readyToBuff = true;
                    _spriteRenderer.color = new Color((float)0.6, 0, 1);
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
                SoundEffectsManager.instance.PlaySFXClip(_activationAudio, (float)0.4);
                _shootingSystem.ChangeFireballSpeed(5);
                buffEnded = false;
                readyToBuff = false;
                _spriteRenderer.color = Color.gray;
                _collider.enabled = false;
            }
        }
    }
}
