using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class HumansGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private int[] _hordesPower;
    [SerializeField] private Human[] humans;
    private bool readyToSpawn;

    private void Start()
    {
        readyToSpawn = true;
    }

    void Update()
    {
        if (readyToSpawn)
        {
            StartCoroutine("GenerateHuman");
        }
    }

    private IEnumerator GenerateHuman()
    {
        readyToSpawn = false;
        //istanzia nemico
        yield return new WaitForSeconds(spawnCooldown);
        readyToSpawn = true;
    }

    private List<int> filterHumansByPower(int power)
    {
        List<int> ret = new List<int>();
        for (int x = 0; x < 5; x++)
        {
            if (humans[x].GetPower() <= power)
            {
                ret.Add(x);
            }
        }
        return ret;
    }
}
