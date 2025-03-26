using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Runtime.CompilerServices;

public class HumansGenerator : MonoBehaviour
{
    public static int level;
    [SerializeField] private Transform _tree;
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private int[] _hordesPower;
    [SerializeField] private GameObject[] _humansPrefabs;
    [SerializeField] private Human[] _humans;
    private float timer;

    private void Start()
    {
        level = 1;
        for (int x = 0; x < _humans.Length; x++)
        {
            _humans[x].SetTree(_tree);
        }
        timer = 0;
    }

    void Update()
    {
        if (!GameManager.pause)
        {
            timer += Time.deltaTime;
            if (timer >= spawnCooldown)
            {
                ShuffleSpawnPoints();
                List<int> selection = SelectHumansToGenerate();
                for(int x = 0; x < selection.Count; x++)
                {
                    Instantiate(_humansPrefabs[selection[x]], _spawnPoints[x].transform.position, Quaternion.identity);
                }
                timer = 0;
            }
        }
    }

    private void ShuffleSpawnPoints()
    {
        for (int i = _spawnPoints.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);

            GameObject temp = _spawnPoints[i];
            _spawnPoints[i] = _spawnPoints[j];
            _spawnPoints[j] = temp;
        }
    }

    private List<int> SelectHumansToGenerate()
    {
        List<int> selection = new List<int>();
        List<int> filtered;
        int power = _hordesPower[level - 1];
        while (power > 0)
        {
            Debug.Log(power);
            filtered = filterHumansByPower(power);
            int sel = Random.Range(0, filtered.Count);
            power -= _humans[sel].GetPower();
            selection.Add(sel);
        }
        Debug.Log(power);
        return selection;
    }

    private List<int> filterHumansByPower(int power)
    {
        List<int> ret = new List<int>();
        for (int x = 0; x < 5; x++)
        {
            if (_humans[x].GetPower() <= power)
            {
                ret.Add(x);
            }
        }
        return ret;
    }
}
