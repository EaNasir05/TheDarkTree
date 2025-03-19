using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject _fireball;
    [SerializeField] private Transform _fireballSpawnPoint;
    [SerializeField] private float fireballCooldown;
    [SerializeField] private int fireballDamage;
    [SerializeField] private float fireballSpeed;
    [SerializeField] private float fireballSize;
    private bool readyToFire;

    private void Start()
    {
        readyToFire = true;
    }

    private void FixedUpdate()
    {
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

    private IEnumerator Shoot()
    {
        if (readyToFire)
        {
            readyToFire = false;
            Instantiate(_fireball, _fireballSpawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(fireballCooldown);
            readyToFire = true;
        }
    }
}
