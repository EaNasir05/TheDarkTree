using System.Collections;
using UnityEngine;

public class TutorialGameManager : MonoBehaviour
{
    public static TutorialGameManager instance;
    [SerializeField] private GameObject _dialogues;
    [SerializeField] private GameObject _objectiveDirection;
    [SerializeField] private GameObject _exit;
    [SerializeField] private SpriteRenderer _keyImage;
    [SerializeField] private Sprite _wasdSprite;
    [SerializeField] private Sprite _eSprite;
    [SerializeField] private Sprite _clickSprite;
    private CorpseInteraction _corpse;
    private static bool next;
    public static int currentDialogue;
    private float _startingCooldown;
    private float startingTimer;
    private bool dialogueStarted;
    private bool moved;
    private bool canGo;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _startingCooldown = 1;
        startingTimer = 0;
        dialogueStarted = false;
        moved = false;
        canGo = false;
    }

    private void Update()
    {
        if (!dialogueStarted)
        {
            startingTimer += Time.deltaTime;
            if (startingTimer >= _startingCooldown)
            {
                dialogueStarted = true;
                StartCoroutine("Dialogue");
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && (currentDialogue == 1 || currentDialogue == 2))
        {
            if (!next)
            {
                next = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentDialogue == 3)
        {
            if (_keyImage.sprite == _clickSprite)
            {
                _keyImage.sprite = null;
            }
        }
        if (currentDialogue == 4)
        {
            if (CorpseManager.instance.GetCorpse() != null && !next)
            {
                next = true;
            }
        }
        if (_corpse != null)
        {
            if (_corpse.GetInteractable())
            {
                _keyImage.sprite = _eSprite;
            }
            else if(CorpseManager.instance.GetCorpse() != null)
            {
                if (!moved)
                {
                    _keyImage.sprite = _wasdSprite;
                }
                else
                {
                    _keyImage.sprite = null;
                }
            }
        }
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            if (!moved)
            {
                moved = true;
                _keyImage.sprite = null;
            }
        }
    }

    public static void NextDialogue()
    {
        next = true;
    }

    private IEnumerator Dialogue()
    {
        currentDialogue = 1;
        _dialogues.transform.GetChild(0).gameObject.SetActive(true);
        //parte audio
        yield return new WaitForSeconds(5);
        _dialogues.transform.GetChild(3).gameObject.SetActive(true);
        next = false;
        yield return new WaitUntil(() => next == true);
        _dialogues.transform.GetChild(3).gameObject.SetActive(false);
        currentDialogue = 3;
        _dialogues.transform.GetChild(0).gameObject.SetActive(false);
        _dialogues.transform.GetChild(1).gameObject.SetActive(true);
        //parte audio
        yield return new WaitForSeconds(5);
        _dialogues.transform.GetChild(4).gameObject.SetActive(true);
        _keyImage.sprite = _clickSprite;
        next = false;
        TutorialHumanGenerator.readyToGenerate = true;
        yield return new WaitUntil(() => next == true);
        _dialogues.transform.GetChild(4).gameObject.SetActive(false);
        _corpse = GameObject.FindGameObjectWithTag("Corpse").GetComponent<CorpseInteraction>();
        next = false;
        currentDialogue = 4;
        _dialogues.transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        _dialogues.transform.GetChild(2).gameObject.SetActive(true);
        //parte audio
        yield return new WaitForSeconds(5);
        moved = false;
        canGo = true;
        _keyImage.sprite = _wasdSprite;
        _dialogues.transform.GetChild(5).gameObject.SetActive(true);
        yield return new WaitUntil(() => (next == true && canGo));
        _objectiveDirection.SetActive(true);
        _exit.SetActive(true);
    }
}
