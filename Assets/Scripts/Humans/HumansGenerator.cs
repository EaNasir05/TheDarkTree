using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEditor;

public class HumansGenerator : MonoBehaviour
{
    public static HumansGenerator instance;
    [SerializeField] private Transform _tree;
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private int[] _hordesPower;
    [SerializeField] private GameObject[] _humansPrefabs;
    [SerializeField] private Human[] _humans;
    private int level;
    private List<GameObject> activeSpawnPoints;
    private float timer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        level = 0;
        activeSpawnPoints = new List<GameObject>();
        for (int x = 0; x < _humans.Length; x++)
        {
            _humans[x].SetTree(_tree);
        }
        timer = 0;
    }

    void Update()
    {
        if (!GameManager.pause && !GameManager.dialogueActive)
        {
            timer += Time.deltaTime;
            if (timer >= spawnCooldown)
            {
                ShuffleSpawnPoints();
                List<int> selection = SelectHumansToGenerate();
                for(int x = 0; x < selection.Count; x++)
                {
                    Instantiate(_humansPrefabs[selection[x]], activeSpawnPoints[x].transform.position, Quaternion.identity);
                }
                timer = 0;
            }
        }
    }

    public void LevelUp()
    {
        level++;
        switch (level)
        {
            case 1:
                activeSpawnPoints.Add(_spawnPoints[0]);
                activeSpawnPoints.Add(_spawnPoints[1]);
                _spawnPoints[0].SetActive(true);
                _spawnPoints[1].SetActive(true);
                break;
            case 2:
                activeSpawnPoints.Add(_spawnPoints[2]);
                activeSpawnPoints.Add(_spawnPoints[3]);
                _spawnPoints[2].SetActive(true);
                _spawnPoints[3].SetActive(true);
                break;
            case 3:
                activeSpawnPoints.Add(_spawnPoints[4]);
                activeSpawnPoints.Add(_spawnPoints[5]);
                _spawnPoints[4].SetActive(true);
                _spawnPoints[5].SetActive(true);
                break;
            case 4:
                activeSpawnPoints.Add(_spawnPoints[6]);
                activeSpawnPoints.Add(_spawnPoints[7]);
                _spawnPoints[6].SetActive(true);
                _spawnPoints[7].SetActive(true);
                break;
            case 5:
                activeSpawnPoints.Add(_spawnPoints[8]);
                activeSpawnPoints.Add(_spawnPoints[9]);
                _spawnPoints[8].SetActive(true);
                _spawnPoints[9].SetActive(true);
                break;
        }
    }

    private void ShuffleSpawnPoints()
    {
        for (int i = activeSpawnPoints.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);

            GameObject temp = activeSpawnPoints[i];
            activeSpawnPoints[i] = activeSpawnPoints[j];
            activeSpawnPoints[j] = temp;
        }
    }

    private List<int> SelectHumansToGenerate()
    {
        List<int> selection = new List<int>();
        List<int> filtered;
        int power = _hordesPower[level];
        while (power > 0)
        {
            filtered = filterHumansByPower(power);
            int sel = Random.Range(0, filtered.Count);
            power -= _humans[filtered[sel]].GetPower();
            selection.Add(filtered[sel]);
        }
        return selection;
    }

    private List<int> filterHumansByPower(int power)
    {
        List<int> ret = new List<int>();
        for (int x = 0; x < 5; x++)
        {
            if (_humans[x].GetPower() <= power && _humans[x].GetLevel() <= level)
            {
                ret.Add(x);
            }
        }
        return ret;
    }
}
