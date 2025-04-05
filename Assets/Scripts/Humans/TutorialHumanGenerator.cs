using UnityEngine;

public class TutorialHumanGenerator : MonoBehaviour
{
    public static bool readyToGenerate;
    [SerializeField] private GameObject _humanPrefab;
    [SerializeField] private TutorialHuman _human;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _spawnPoint;

    private void Start()
    {
        _human.SetTarget(_target);
        readyToGenerate = false;
    }

    private void Update()
    {
        if (readyToGenerate)
        {
            GenerateHuman();
        }
    }

    private void GenerateHuman()
    {
        GameObject.Instantiate(_humanPrefab, _spawnPoint.position, Quaternion.identity);
        readyToGenerate = false;
    }
}
