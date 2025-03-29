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

    public int GetFireballDamage() { return fireballDamage; }
    public float GetFireballSpeed() { return fireballSpeed; }
    public float GetFireballSize() { return fireballSize; }
    public void ChangeFireballCooldown(float value) { fireballCooldown += value; }
    public void ChangeFireballDamage(int value) { fireballDamage += value; }
    public void ChangeFireballSize(float value) { fireballSize += value; }

    private void Start()
    {
        readyToFire = true;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && !GameManager.pause && !GameManager.selectingTrap && CorpseManager.instance.GetCorpse() == null)
        {
            StartCoroutine("Shoot");
        }
    }

    private IEnumerator Shoot()
    {
        if (readyToFire)
        {
            readyToFire = false;
            Instantiate(_fireball, _fireballSpawnPoint.position, Quaternion.identity);
            if(fireballCooldown >= 0.05)
            {
                yield return new WaitForSeconds(fireballCooldown);
            }
            else
            {
                yield return new WaitForSeconds((float)0.05);
            }
            readyToFire = true;
        }
    }
}
