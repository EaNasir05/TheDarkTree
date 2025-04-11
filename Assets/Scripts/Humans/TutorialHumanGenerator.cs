using UnityEngine;

public class TutorialHumanGenerator : MonoBehaviour
{
    public static bool readyToGenerate;
    public static int kills;
    [SerializeField] private GameObject _humanPrefab;
    [SerializeField] private TutorialHuman _human;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _spawnPoint;
    public static bool ready;
    public static bool useCooldown;
    private float timer;

    private void Start()
    {
        _human.SetTarget(_target);
        readyToGenerate = false;
        ready = true;
        timer = 0;
    }

    private void Update()
    {
        if (useCooldown)
        {
            if (readyToGenerate && !GameManager.pause)
            {
                timer += Time.deltaTime;
                if (timer >= 5)
                {
                    GenerateHuman();
                    timer = 0;
                }
            }
        }
        else
        {
            if (readyToGenerate && ready)
            {
                GenerateHuman();
            }
        }
    }

    private void GenerateHuman()
    {
        GameObject.Instantiate(_humanPrefab, _spawnPoint.position, Quaternion.identity);
        ready = false;
    }
}
