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
        "FINCHÉ LO STREGONE È NELLA RUNA, LA SUA FREQUENZA DI SPARO È AUMENTATA",
        "SE LO STREGONE SI SPOSTA SOPRA QUESTA TRAPPOLA, TUTTI GLI UMANI AL SUO INTERNO SUBISCONO DANNI NEL TEMPO",
        "RALLENTA IL MOVIMENTO DEGLI UMANI CHE CI CAPITANO SOPRA",
        "SE COLPITO DALLA PALLA DI FUOCO ESPLODE, INFLIGGENDO DANNI AGLI UMANI VICINI",
        "SE COLPITO DALLA PALLA DI FUOCO ESPLODE, STORDENDO GLI UMANI VICINI",
        "SE COLPITO DALLA PALLA DI FUOCO, AUMENTA PER POCHI SECONDI LA VELOCITÀ DELLA PALLA DI FUOCO"
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
    [SerializeField] private GameObject _keyImage;
    [SerializeField] private Sprite _eKeySprite;
    [SerializeField] private Sprite _qKeySprite;
    [SerializeField] private GameObject _dialogueWall;
    [SerializeField] private GameObject _tutorialRoot;
    [SerializeField] private GameObject _tutorialSpawnPoint;
    [SerializeField] private GameObject _battlegroundEntrance;
    [SerializeField] private GameObject _objectiveDirection;
    [SerializeField] private GameObject _tutorialUIBackground;
    private GameObject _dialogues;
    private GameObject _objectives;
    private GameObject _examples;
    private int currentDialogue;
    private bool nextDialogue;
    [SerializeField] private AudioClip[] _dialoguesAudios;
    [SerializeField] private AudioClip[] _feedRootAudio;
    [SerializeField] private AudioClip _treeDamagedAudio;
    [SerializeField] private AudioClip _levelUpAudio;
    [SerializeField] private AudioClip _spawnPointAudio;

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
            TutorialHumanGenerator.readyToGenerate = false;
            TutorialHumanGenerator.useCooldown = true;
            Cursor.visible = false;
            _dialogues = GameObject.FindGameObjectWithTag("Dialogues");
            _objectives = GameObject.FindGameObjectWithTag("Objectives");
            _examples = GameObject.FindGameObjectWithTag("Examples");
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
        if (Input.GetKeyDown(KeyCode.Space) && tutorial && (currentDialogue == 2 || currentDialogue == 4 || currentDialogue == 5 || currentDialogue == 6 || currentDialogue == 10 || currentDialogue == 11 || currentDialogue == 12))
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
        if (currentDialogue == 1 || currentDialogue == 9)
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
            if (tutorial && currentDialogue == 9)
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
        pause = true;
        if (tutorial)
        {
            nextDialogue = true;
        }
        else
        {
            ResetCursor();
            Cursor.visible = true;
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
    }

    private void TutorialLevelUp()
    {
        ResetCursor();
        Cursor.visible = true;
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

    public void FeedRoot()
    {
        corpses++;
        UserInterfaceManager.instance.IncreaseCorpsesCount();
        if (!tutorial)
        {
            SoundEffectsManager.instance.PlaySFXClipOnFeed(_feedRootAudio[Random.Range(0, 3)], 1);
        }
        if (_milestones[level] == corpses)
        {
            corpses = 0;
            LevelUp();
        }
        if (tutorial)
        {
            if(currentDialogue == 1)
            {
                currentDialogue = 2;
                _keyImage.SetActive(false);
                nextDialogue = true;
            }
        }
    }

    private IEnumerator StartDialogue()
    {
        selectingTrap = true;
        yield return new WaitForSeconds((float)0.5);
        currentDialogue = 1;
        _dialogues.transform.GetChild(0).gameObject.SetActive(true);
        SoundEffectsManager.instance.PlayDialogue(_dialoguesAudios[0], 1);
        _keyImage.GetComponent<SpriteRenderer>().sprite = _eKeySprite;
        yield return new WaitForSeconds(_dialoguesAudios[0].length);
        _objectives.transform.GetChild(0).gameObject.SetActive(true);
        _examples.transform.GetChild(0).gameObject.SetActive(true);
        selectingTrap = false;
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);

        selectingTrap = true;
        Cursor.visible = false;
        _dialogues.transform.GetChild(0).gameObject.SetActive(false);
        _objectives.transform.GetChild(0).gameObject.SetActive(false);
        _examples.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds((float)0.5);
        _dialogues.transform.GetChild(1).gameObject.SetActive(true);
        SoundEffectsManager.instance.PlayDialogue(_dialoguesAudios[1], 1);
        yield return new WaitForSeconds(_dialoguesAudios[1].length);
        _objectives.transform.GetChild(1).gameObject.SetActive(true);
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);

        _dialogues.transform.GetChild(1).gameObject.SetActive(false);
        _objectives.transform.GetChild(1).gameObject.SetActive(false);
        currentDialogue = 3;
        _dialogues.transform.GetChild(2).gameObject.SetActive(true);
        SoundEffectsManager.instance.PlayDialogue(_dialoguesAudios[2], 1);
        yield return new WaitForSeconds(_dialoguesAudios[2].length);
        selectingTrap = false;
        Cursor.visible = true;
        _objectives.transform.GetChild(2).gameObject.SetActive(true);
        _examples.transform.GetChild(1).gameObject.SetActive(true);
        _tutorialSpawnPoint.SetActive(true);
        SoundEffectsManager.instance.PlaySFXClip(_spawnPointAudio, (float)0.5);
        TutorialHumanGenerator.readyToGenerate = true;
        //yield return new WaitUntil(() => TutorialHumanGenerator.kills == 3);
        //TutorialHumanGenerator.readyToGenerate = false;
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);

        TutorialHumanGenerator.readyToGenerate = false;
        UserInterfaceManager.instance.ResetCorpsesCount();
        UserInterfaceManager.instance.SetCorpsesMaxValue(_milestones[level]);
        GameObject[] remainingHumans = GameObject.FindGameObjectsWithTag("Tutorial Enemy");
        GameObject[] remainingCorpses = GameObject.FindGameObjectsWithTag("Corpse");
        foreach (var human in remainingHumans)
        {
            Destroy(human);
        }
        foreach (var corpse in remainingCorpses)
        {
            Destroy(corpse);
        }
        currentDialogue = 4;
        Cursor.visible = false;
        _dialogues.transform.GetChild(2).gameObject.SetActive(false);
        _objectives.transform.GetChild(2).gameObject.SetActive(false);
        _examples.transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds((float)0.5);
        _dialogues.transform.GetChild(3).gameObject.SetActive(true);
        SoundEffectsManager.instance.PlayDialogue(_dialoguesAudios[3], 1);
        yield return new WaitForSeconds(_dialoguesAudios[3].length);
        nextDialogue = false;
        _objectives.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitUntil(() => nextDialogue == true);

        _dialogues.transform.GetChild(3).gameObject.SetActive(false);
        _objectives.transform.GetChild(1).gameObject.SetActive(false);
        currentDialogue = 5;
        _dialogues.transform.GetChild(4).gameObject.SetActive(true);
        SoundEffectsManager.instance.PlayDialogue(_dialoguesAudios[4], 1);
        yield return new WaitForSeconds(_dialoguesAudios[4].length);
        _objectives.transform.GetChild(1).gameObject.SetActive(true);
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);

        _dialogues.transform.GetChild(4).gameObject.SetActive(false);
        _objectives.transform.GetChild(1).gameObject.SetActive(false);
        currentDialogue = 6;
        _dialogues.transform.GetChild(5).gameObject.SetActive(true);
        SoundEffectsManager.instance.PlayDialogue(_dialoguesAudios[5], 1);
        yield return new WaitForSeconds(_dialoguesAudios[5].length);
        _objectives.transform.GetChild(1).gameObject.SetActive(true);
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);

        _tutorialUIBackground.SetActive(false);
        _dialogues.transform.GetChild(5).gameObject.SetActive(false);
        _objectives.transform.GetChild(1).gameObject.SetActive(false);
        currentDialogue = 7;
        nextDialogue = false;
        TutorialLevelUp();
        yield return new WaitUntil(() => nextDialogue == true);

        selectingTrap = true;
        Cursor.visible = false;
        _tutorialUIBackground.SetActive(true);
        _tutorialRoot.SetActive(false);
        currentDialogue = 8;
        yield return new WaitForSeconds((float)0.5);
        _dialogues.transform.GetChild(6).gameObject.SetActive(true);
        SoundEffectsManager.instance.PlayDialogue(_dialoguesAudios[6], 1);
        yield return new WaitForSeconds(_dialoguesAudios[6].length);
        selectingTrap = false;
        Cursor.visible = true;
        _objectives.transform.GetChild(3).gameObject.SetActive(true);
        nextDialogue = false;
        _objectiveDirection.SetActive(true);
        _battlegroundEntrance.SetActive(true);
        yield return new WaitUntil(() => nextDialogue == true);

        _dialogues.transform.GetChild(6).gameObject.SetActive(false);
        _objectives.transform.GetChild(3).gameObject.SetActive(false);
        selectingTrap = true;
        Cursor.visible = false;
        currentDialogue = 9;
        yield return new WaitForSeconds((float)0.5);
        _dialogues.transform.GetChild(7).gameObject.SetActive(true);
        SoundEffectsManager.instance.PlayDialogue(_dialoguesAudios[7], 1);
        _keyImage.GetComponent<SpriteRenderer>().sprite = _qKeySprite;
        yield return new WaitForSeconds(_dialoguesAudios[7].length);
        selectingTrap = false;
        Cursor.visible = true;
        _objectives.transform.GetChild(4).gameObject.SetActive(true);
        _examples.transform.GetChild(2).gameObject.SetActive(true);
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);

        _keyImage.SetActive(false);
        _dialogues.transform.GetChild(7).gameObject.SetActive(false);
        _objectives.transform.GetChild(4).gameObject.SetActive(false);
        _examples.transform.GetChild(2).gameObject.SetActive(false);
        selectingTrap = true;
        Cursor.visible = false;
        currentDialogue = 10;
        yield return new WaitForSeconds((float)0.5);
        _dialogues.transform.GetChild(8).gameObject.SetActive(true);
        _examples.transform.GetChild(3).gameObject.SetActive(true);
        SoundEffectsManager.instance.PlayDialogue(_dialoguesAudios[8], 1);
        yield return new WaitForSeconds(_dialoguesAudios[8].length);
        _objectives.transform.GetChild(1).gameObject.SetActive(true);
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);

        _dialogues.transform.GetChild(8).gameObject.SetActive(false);
        _objectives.transform.GetChild(1).gameObject.SetActive(false);
        currentDialogue = 11;
        _dialogues.transform.GetChild(9).gameObject.SetActive(true);
        SoundEffectsManager.instance.PlayDialogue(_dialoguesAudios[9], 2);
        yield return new WaitForSeconds(_dialoguesAudios[9].length);
        _objectives.transform.GetChild(1).gameObject.SetActive(true);
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);

        _dialogues.transform.GetChild(9).gameObject.SetActive(false);
        _objectives.transform.GetChild(1).gameObject.SetActive(false);
        _examples.transform.GetChild(3).gameObject.SetActive(false);
        currentDialogue = 12;
        _dialogues.transform.GetChild(10).gameObject.SetActive(true);
        _examples.transform.GetChild(4).gameObject.SetActive(true);
        SoundEffectsManager.instance.PlayDialogue(_dialoguesAudios[10], 1);
        yield return new WaitForSeconds(_dialoguesAudios[10].length);
        _objectives.transform.GetChild(1).gameObject.SetActive(true);
        nextDialogue = false;
        yield return new WaitUntil(() => nextDialogue == true);

        _dialogues.transform.GetChild(10).gameObject.SetActive(false);
        _objectives.transform.GetChild(1).gameObject.SetActive(false);
        _examples.transform.GetChild(4).gameObject.SetActive(false);
        selectingTrap = false;
        Cursor.visible = true;
        _dialogueWall.SetActive(false);
        yield return new WaitForSeconds(1);
        tutorial = false;
        HumansGenerator.instance.LevelUp();
    }

    public void GoToNextDialogue()
    {
        nextDialogue = true;
    }
}
