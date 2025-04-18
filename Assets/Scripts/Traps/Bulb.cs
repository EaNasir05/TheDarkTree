using UnityEngine;
using System.Collections;

public class Bulb : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private float explosionCooldown;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _explosionSprites;
    [SerializeField] private AudioClip _explosionAudio;
    [SerializeField] private float _explosionVolume;
    [SerializeField] private float[] _color;
    private bool readyToExplode = true;
    private float timer = 0;

    private void Update()
    {
        if (!readyToExplode && !GameManager.pause)
        {
            if (timer >= explosionCooldown)
            {
                readyToExplode = true;
                _collider.enabled = true;
                _spriteRenderer.color = new Color(_color[0], _color[1], _color[2]);
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
            _collider.enabled = false;
            readyToExplode = false;
            _spriteRenderer.color = Color.gray;
        }
    }

    private IEnumerator CreateExplosion()
    {
        SoundEffectsManager.instance.PlaySFXClip(_explosionAudio, _explosionVolume);
        GameObject explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
        yield return new WaitForSeconds((float)0.10);
        explosion.GetComponent<SpriteRenderer>().sprite = _explosionSprites[0];
        yield return new WaitForSeconds((float)0.10);
        explosion.GetComponent<SpriteRenderer>().sprite = _explosionSprites[1];
        yield return new WaitForSeconds((float)0.05);
        Destroy(explosion);
    }
}
