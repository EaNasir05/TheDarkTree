using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool pause;
    public static bool selectingTrap;
    public static bool dialogueActive;
    private int level = 0;
    private int corpses = 0;
    private int healthPoints = 100;
    [SerializeField] private GameObject _tree;
    [SerializeField] private int[] _milestones;
    [SerializeField] private Shooting _shootingSystem;
    [SerializeField] private Movement _movementSystem;
    [SerializeField] private GameObject[] _roots;
    private int[] trapsAmounts = {0, 0, 0, 0, 0, 0};
    private int[] enhancementsAmounts = { 0, 0, 0, 0 };
    private string[] _trapsNames = {
        "RUNA MAGICA",
        "ROVI SANGUINARI",
        "MELMA OSCURA",
        "BULBO ESPLOSIVO",
        "BULBO STORDENTE",
        "CRISTALLO ARCANO"
    };
    private string[] _trapsDescriptions = {
        "FINCHÉ LO STREGONE È NELLA RUNA, LA SUA FREQUENZA DI SPARO È AUMENTATA",
        "SE LO STREGONE SI SPOSTA SOPRA QUESTA TRAPPOLA, TUTTI GLI UMANI NEI ROVI PRENDONO DANNI NEL TEMPO",
        "RALLENTA IL MOVIMENTO DEGLI UMANI CHE CI CAPITANO SOPRA",
        "SE COLPITO DALLA PALLA DI FUOCO ESPLODE, INFLIGGENDO DANNI AGLI UMANI VICINI",
        "SE COLPITO DALLA PALLA DI FUOCO ESPLODE, STORDENDO GLI UMANI VICINI",
        "SE COLPITO DALLA PALLA DI FUOCO, AUMENTA PER POCHI SECONDI IL DANNO INFLITTO AGLI UMANI"
    };
    //private Sprite[] _trapsSprites = { };
    private string[] _enhancementsNames = {
        "PIEDE LESTO",
        "DEVASTAZIONE",
        "PIROMANZIA",
        "PATTO OSCURO"
    };
    private string[] _enhancementsDescriptions = {
        "AUMENTA LA VELOCITÀ DI MOVIMENTO DELLO STREGONE",
        "AUMENTA LA FREQUENZA DI SPARO DELLE PALLE DI FUOCO",
        "AUMENTA LA DIMENSIONE DELLE PALLE DI FUOCO",
        "AUMENTA IL DANNO DELLE PALLE DI FUOCO"
    };
    //private Sprite[] _enhancementsSprites = { };
    private GameObject currentRoot;
    private int[] trapsGenerated;
    private int[] enhancementsGenerated;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        pause = false;
        selectingTrap = false;
        dialogueActive = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
        }
    }

    public void DecreaseHealthPoints(int damage)
    {
        UserInterfaceManager.instance.DecreaseHP(damage);
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void RootInteraction(GameObject root)
    {
        currentRoot = root;
        UserInterfaceManager.instance.OpenTrapsList();
        selectingTrap = true;
    }

    public void TrapSelection(int index)
    {
        if (trapsAmounts[index] > 0)
        {
            currentRoot.GetComponent<Root>().PlaceTrap(index);
            trapsAmounts[index]--;
            selectingTrap = false;
            UserInterfaceManager.instance.CloseTrapsList();
            UserInterfaceManager.instance.UpdateTrapsList(index, trapsAmounts[index]);
        }
        else
        {
            Debug.Log("NON HAI TRAPPOLE DI QUESTO TIPO");
        }
    }

    public void endTrapSelection()
    {
        UserInterfaceManager.instance.CloseTrapsList();
        selectingTrap = false;
    }

    public void IncreaseTrapAmount(int index)
    {
        int trap = trapsGenerated[index];
        trapsAmounts[trap]++;
        UserInterfaceManager.instance.UpdateTrapsList(trap, trapsAmounts[trap]);
        UserInterfaceManager.instance.CloseLevelUpTraps();
        UserInterfaceManager.instance.OpenLevelUpEnhancements();
    }

    public void BuffPlayer(int index)
    {
        int enhancement = enhancementsGenerated[index];
        enhancementsAmounts[enhancement]++;
        switch (enhancement)
        {
            case 0:
                _movementSystem.ChangeMovementSpeed((float)1.5);
                break;
            case 1:
                _shootingSystem.ChangeFireballCooldown((float)-0.133);
                break;
            case 2:
                _shootingSystem.ChangeFireballSize((float)0.2);
                break;
            case 3:
                _shootingSystem.ChangeFireballDamage(2);
                break;
            default:
                Debug.Log("SIGILLO INESISTENTE");
                break;
        }
        UserInterfaceManager.instance.CloseLevelUpEnhancements();
        pause = false;
        if (selectingTrap)
        {
            selectingTrap = false;
        }
        AddRoots();
        HumansGenerator.instance.LevelUp();
    }

    private void AddRoots()
    {
        switch (level)
        {
            case 1:
                _roots[0].SetActive(true);
                _roots[1].SetActive(true);
                break;
            case 3:
                _roots[2].SetActive(true);
                _roots[3].SetActive(true);
                break;
            case 4:
                _roots[4].SetActive(true);
                break;
            case 5:
                _roots[5].SetActive(true);
                _roots[6].SetActive(true);
                break;
            case 7:
                _roots[7].SetActive(true);
                _roots[8].SetActive(true);
                break;
            case 8:
                _roots[9].SetActive(true);
                break;
        }
    }

    private int[] GenerateTraps()
    {
        int[] traps = {-1, -1};
        for (int x = 0; x < 2; x++)
        {
            traps[x] = Random.Range(0, 6);
            if (x == 1 && traps[0] == traps[1])
            {
                x--;
            }
        }
        return traps;
    }

    private int[] GenerateEnhancements()
    {
        int[] enhancements = {-1, -1};
        for (int x = 0; x < 2; x++)
        {
            enhancements[x] = Random.Range(0, 4);
            if (x == 1 && enhancements[0] == enhancements[1] && enhancementsAmounts[x] < 4)
            {
                x--;
            }
        }
        return enhancements;
    }

    private void LevelUp()
    {
        UserInterfaceManager.instance.IncreaseLevel();
        level++;
        pause = true;
        if (level < 10)
        {
            trapsGenerated = GenerateTraps();
            enhancementsGenerated = GenerateEnhancements();
            UserInterfaceManager.instance.GenerateLevelUpTraps(_trapsNames[trapsGenerated[0]], _trapsNames[trapsGenerated[1]], _trapsDescriptions[trapsGenerated[0]], _trapsDescriptions[trapsGenerated[1]]);
            UserInterfaceManager.instance.GenerateLevelUpEnhancements(_enhancementsNames[enhancementsGenerated[0]], _enhancementsNames[enhancementsGenerated[1]], _enhancementsDescriptions[enhancementsGenerated[0]], _enhancementsDescriptions[enhancementsGenerated[1]]);
            UserInterfaceManager.instance.OpenLevelUpTraps();
        }
        else
        {
            SceneManager.LoadScene("Victory");
        }
    }

    public void FeedRoot()
    {
        corpses++;
        if (_milestones[level] == corpses)
        {
            LevelUp();
            corpses = 0;
        }
    }
}
