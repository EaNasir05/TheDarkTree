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
    private int fireRateLevel;
    private bool readyToFire;

    public int GetFireballDamage() { return fireballDamage; }
    public float GetFireballSpeed() { return fireballSpeed; }
    public float GetFireballSize() { return fireballSize; }
    public void ChangeFireballDamage(int value) { fireballDamage += value; }
    public void ChangeFireballSize(float value) { fireballSize += value; }

    public void IncreaseFireRate()
    {
        fireRateLevel++;
        ChangeFireballCooldown();
    }

    public void DecreaseFireRate()
    {
        fireRateLevel--;
        ChangeFireballCooldown();
    }

    private void ChangeFireballCooldown()
    {
        switch (fireRateLevel)
        {
            case 1:
                fireballCooldown = (float)0.8;
                break;
            case 2:
                fireballCooldown = (float)0.6;
                break;
            case 3:
                fireballCooldown = (float)0.5;
                break;
            case 4:
                fireballCooldown = (float)0.4;
                break;
            case 5:
                fireballCooldown = (float)0.34;
                break;
            case 6:
                fireballCooldown = (float)0.3;
                break;
            case 7:
                fireballCooldown = (float)0.27;
                break;
            default:
                Debug.Log("INVALID FIRE RATE LEVEL");
                break;
        }
    }

    private void Start()
    {
        readyToFire = true;
        fireRateLevel = 1;
    }

    private void Update()
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
