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
    [SerializeField] private Texture2D _shootingCursor;
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
        "FINCH� LO STREGONE � NELLA RUNA, LA SUA FREQUENZA DI SPARO � AUMENTATA",
        "SE LO STREGONE SI SPOSTA SOPRA QUESTA TRAPPOLA, TUTTI GLI UMANI AL SUO INTERNO SUBISCONO DANNI NEL TEMPO",
        "RALLENTA IL MOVIMENTO DEGLI UMANI CHE CI CAPITANO SOPRA",
        "SE COLPITO DALLA PALLA DI FUOCO ESPLODE, INFLIGGENDO DANNI AGLI UMANI VICINI",
        "SE COLPITO DALLA PALLA DI FUOCO ESPLODE, STORDENDO GLI UMANI VICINI",
        "SE COLPITO DALLA PALLA DI FUOCO, AUMENTA PER POCHI SECONDI LA VELOCIT� DELLA PALLA DI FUOCO"
    };
    //private Sprite[] _trapsSprites = { };
    private string[] _enhancementsNames = {
        "PIEDE LESTO",
        "DEVASTAZIONE",
        "PIROMANZIA",
        "PATTO OSCURO"
    };
    private string[] _enhancementsDescriptions = {
        "AUMENTA LA VELOCIT� DI MOVIMENTO DELLO STREGONE",
        "AUMENTA LA FREQUENZA DI SPARO DELLE PALLE DI FUOCO",
        "AUMENTA LA DIMENSIONE DELLE PALLE DI FUOCO",
        "AUMENTA IL DANNO DELLE PALLE DI FUOCO"
    };
    //private Sprite[] _enhancementsSprites = { };
    private GameObject currentRoot;
    private int[] trapsGenerated;
    private int[] enhancementsGenerated;
    [SerializeField] private GameObject _keyImage;
    [SerializeField] private GameObject _dialogueWall;
    [SerializeField] private GameObject _tutorialRoot;
    [SerializeField] private GameObject _battlegroundEntrance;
    [SerializeField] private GameObject _objectiveDirection;
    private GameObject _dialogues;
    private GameObject _objectives;
    private int currentDialogue;
    private bool nextDialogue;
    [SerializeField] private AudioClip[] _feedRootAudio;
    [SerializeField] private AudioClip _treeDamagedAudio;
    [SerializeField] private AudioClip _levelUpAudio;

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
            _objectives = GameObject.FindGameObjectWithTag("Objectives");
            currentDialogue = 0;
            nextDialogue = false;
            StartCoroutine("StartDialogue");
            _dialogueWall.SetActive(true);
        }
        SetShootingCursor();
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

    private void SetShootingCursor()
    {
        Cursor.SetCursor(_shootingCursor, Vector2.zero, CursorMode.Auto);
    }

    private void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void DecreaseHealthPoints(int damage)
    {
        SoundEffectsManager.instance.PlaySFXClip(_treeDamagedAudio, 1);
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
        ResetCursor();
        UserInterfaceManager.instance.OpenTrapsList();
        pause = true;
        if (tutorial)
        {
            _keyImage.SetActive(false);
        }
    }

    public void RootTutorialCheck()
    {
        if (currentDialogue == 1 || currentDialogue == 4)
        {
            _keyImage.SetActive(true);
        }
    }

    public void TrapSelection(int index)
    {
        if (trapsAmounts[index] > 0)
        {
            currentRoot.GetComponent<Root>().PlaceTrap(index);
            trapsAmounts[index]--;
            pause = false;
            SetShootingCursor();
            UserInterfaceManager.instance.CloseTrapsList();
            UserInterfaceManager.instance.DecreaseTrapsCount();
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
        SetShootingCursor();
        pause = false;
    }

    public void IncreaseTrapAmount(int index)
    {
        int trap = trapsGenerated[index];
        trapsAmounts[trap]++;
        UserInterfaceManager.instance.UpdateTrapsList(trap, trapsAmounts[trap]);
        UserInterfaceManager.instance.CloseLevelUpTraps();
        UserInterfaceManager.instance.IncreaseTrapsCount();
        UserInterfaceManager.instance.OpenLevelUpEnhancements();
        if (tutorial)
        {
            _objectives.transform.GetChild(0).gameObject.SetActive(false);
        }
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
        if (!tutorial)
        {
            UserInterfaceManager.instance.ShowHP();
        }
        UserInterfaceManager.instance.ShowLevel();
        SetShootingCursor();
        pause = false;
        AddRoots();
        if (tutorial)
        {
            currentDialogue = 2;
            _keyImage.SetActive(false);
            nextDialogue = true;
        }
        else
        {
            HumansGenerator.instance.LevelUp();
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
        SoundEffectsManager.instance.PlaySFXClip(_levelUpAudio, 1);
        UserInterfaceManager.instance.IncreaseLevel();
        level++;
        Cursor.visible = true;
        pause = true;
        ResetCursor();
        if (tutorial)
        {
            _dialogues.transform.GetChild(0).gameObject.SetActive(false);
            _objectives.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (level < 10)
        {
            UserInterfaceManager.instance.ResetCorpsesCount();
            UserInterfaceManager.instance.SetCorpsesMaxValue(_milestones[level]);
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
        UserInterfaceManager.instance.IncreaseCorpsesCount();
        if (_milestones[level] == corpses)
        {
            corpses = 0;
            LevelUp();
        }
    }

    private IEnumerator StartDialogue()
    {
        selectingTrap = true;
        yield return new WaitForSeconds(1);
        currentDialogue = 1;
        _dialogues.transform.GetChild(0).gameObject.SetActive(true);
        //parte audio
        yield return new WaitForSeconds(5);
        _objectives.transform.GetChild(0).gameObject.SetActive(true);
        selectingTrap = false;
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);
        selectingTrap = true;
        yield return new WaitForSeconds(1);
        _dialogues.transform.GetChild(1).gameObject.SetActive(true);
        //parte audio
        yield return new WaitForSeconds(5);
        _objectives.transform.GetChild(1).gameObject.SetActive(true);
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);
        _dialogues.transform.GetChild(1).gameObject.SetActive(false);
        _objectives.transform.GetChild(1).gameObject.SetActive(false);
        currentDialogue = 3;
        _dialogues.transform.GetChild(2).gameObject.SetActive(true);
        //parte audio
        yield return new WaitForSeconds(5);
        _objectives.transform.GetChild(1).gameObject.SetActive(true);
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);
        _dialogues.transform.GetChild(2).gameObject.SetActive(false);
        _objectives.transform.GetChild(1).gameObject.SetActive(false);
        currentDialogue = 4;
        _dialogues.transform.GetChild(3).gameObject.SetActive(true);
        //parte audio
        yield return new WaitForSeconds(5);
        selectingTrap = false;
        _keyImage.SetActive(true);
        _objectives.transform.GetChild(2).gameObject.SetActive(true);
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);
        _dialogues.transform.GetChild(3).gameObject.SetActive(false);
        _objectives.transform.GetChild(2).gameObject.SetActive(false);
        currentDialogue = 5;
        yield return new WaitForSeconds(1);
        _dialogues.transform.GetChild(4).gameObject.SetActive(true);
        //parte audio
        yield return new WaitForSeconds(5);
        _objectives.transform.GetChild(1).gameObject.SetActive(true);
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);
        _dialogues.transform.GetChild(4).gameObject.SetActive(false);
        _objectives.transform.GetChild(1).gameObject.SetActive(false);
        currentDialogue = 6;
        _dialogues.transform.GetChild(5).gameObject.SetActive(true);
        //parte audio
        yield return new WaitForSeconds(5);
        _objectives.transform.GetChild(1).gameObject.SetActive(true);
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);
        _dialogues.transform.GetChild(5).gameObject.SetActive(false);
        _objectives.transform.GetChild(1).gameObject.SetActive(false);
        _dialogueWall.SetActive(false);
        yield return new WaitForSeconds(1);
        tutorial = false;
        HumansGenerator.instance.LevelUp();
    }
}
