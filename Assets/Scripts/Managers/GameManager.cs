using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool pause;
    public static bool selectingTrap;
    public static bool tutorial;
    private int level = 0;
    [SerializeField] private bool tutorialBattleground;
    [SerializeField] private int corpses = 0;
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
    [SerializeField] private SpriteRenderer _keyImage;
    [SerializeField] private Sprite _qKeySprite;
    private GameObject _dialogues;
    private int currentDialogue;
    private bool nextDialogue;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Start()
    {
        pause = false;
        selectingTrap = false;
        tutorial = tutorialBattleground;
        if (tutorial)
        {
            Cursor.visible = false;
            _dialogues = GameObject.FindGameObjectWithTag("Dialogues");
            currentDialogue = 0;
            nextDialogue = false;
            StartCoroutine("StartDialogue");
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
        }
        if (Input.GetKeyDown(KeyCode.Space) && (currentDialogue == 2 || currentDialogue == 3 || currentDialogue == 5 || currentDialogue == 6))
        {
            if (!nextDialogue)
            {
                nextDialogue = true;
            }
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
        Cursor.visible = true;
        UserInterfaceManager.instance.OpenTrapsList();
        selectingTrap = true;
        pause = true;
    }

    public void RootTutorialCheck()
    {
        if (currentDialogue == 1)
        {
            _keyImage.sprite = _qKeySprite;
        }
    }

    public void TrapSelection(int index)
    {
        if (trapsAmounts[index] > 0)
        {
            currentRoot.GetComponent<Root>().PlaceTrap(index);
            trapsAmounts[index]--;
            selectingTrap = false;
            pause = false;
            UserInterfaceManager.instance.CloseTrapsList();
            UserInterfaceManager.instance.UpdateTrapsList(index, trapsAmounts[index]);
            if (tutorial && currentDialogue == 4)
            {
                nextDialogue = true;
            }
        }
        else
        {
            Debug.Log("NON HAI TRAPPOLE DI QUESTO TIPO");
        }
    }

    public void EndTrapSelection()
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
                _shootingSystem.IncreaseFireRate();
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
        UserInterfaceManager.instance.ShowHP();
        UserInterfaceManager.instance.ShowLevel();
        pause = false;
        if (selectingTrap)
        {
            selectingTrap = false;
        }
        AddRoots();
        HumansGenerator.instance.LevelUp();
        if (tutorial)
        {
            currentDialogue = 2;
            _keyImage.sprite = null;
            nextDialogue = true;
        }
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
                break;
            case 4:
                _roots[3].SetActive(true);
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
            if (x == 1 && (enhancements[0] == enhancements[1] || enhancementsAmounts[x] > 4))
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
        Cursor.visible = true;
        pause = true;
        if (tutorial)
        {
            _dialogues.transform.GetChild(0).gameObject.SetActive(false);
            _dialogues.transform.GetChild(6).gameObject.SetActive(false);
        }
        if (level < 10)
        {
            UserInterfaceManager.instance.HideHP();
            UserInterfaceManager.instance.HideLevel();
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

    private IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(1);
        currentDialogue = 1;
        _dialogues.transform.GetChild(0).gameObject.SetActive(true);
        //parte audio
        yield return new WaitForSeconds(5);
        _dialogues.transform.GetChild(6).gameObject.SetActive(true);
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);
        yield return new WaitForSeconds(1);
        _dialogues.transform.GetChild(1).gameObject.SetActive(true);
        //parte audio
        yield return new WaitForSeconds(5);
        _dialogues.transform.GetChild(7).gameObject.SetActive(true);
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);
        _dialogues.transform.GetChild(1).gameObject.SetActive(false);
        _dialogues.transform.GetChild(7).gameObject.SetActive(false);
        currentDialogue = 3;
        _dialogues.transform.GetChild(2).gameObject.SetActive(true);
        //parte audio
        yield return new WaitForSeconds(5);
        _dialogues.transform.GetChild(7).gameObject.SetActive(true);
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);
        _dialogues.transform.GetChild(2).gameObject.SetActive(false);
        _dialogues.transform.GetChild(7).gameObject.SetActive(false);
        currentDialogue = 4;
        _dialogues.transform.GetChild(3).gameObject.SetActive(true);
        //parte audio
        yield return new WaitForSeconds(5);
        _dialogues.transform.GetChild(8).gameObject.SetActive(true);
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);
        _dialogues.transform.GetChild(3).gameObject.SetActive(false);
        _dialogues.transform.GetChild(8).gameObject.SetActive(false);
        currentDialogue = 5;
        yield return new WaitForSeconds(1);
        _dialogues.transform.GetChild(4).gameObject.SetActive(true);
        //parte audio
        yield return new WaitForSeconds(5);
        _dialogues.transform.GetChild(7).gameObject.SetActive(true);
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);
        _dialogues.transform.GetChild(4).gameObject.SetActive(false);
        _dialogues.transform.GetChild(7).gameObject.SetActive(false);
        currentDialogue = 6;
        _dialogues.transform.GetChild(5).gameObject.SetActive(true);
        //parte audio
        yield return new WaitForSeconds(5);
        _dialogues.transform.GetChild(7).gameObject.SetActive(true);
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);
        _dialogues.transform.GetChild(5).gameObject.SetActive(false);
        _dialogues.transform.GetChild(7).gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        tutorial = false;
    }
}
