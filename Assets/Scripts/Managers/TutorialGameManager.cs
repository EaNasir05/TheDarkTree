using System.Collections;
using UnityEngine;

public class TutorialGameManager : MonoBehaviour
{
    public static TutorialGameManager instance;
    [SerializeField] private GameObject _dialogues;
    private static bool next;
    public static int currentDialogue;
    private float _startingCooldown;
    private float startingTimer;
    private bool dialogueStarted;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _startingCooldown = 1;
        startingTimer = 0;
        dialogueStarted = false;
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
        yield return new WaitForSeconds(1);
        _dialogues.transform.GetChild(4).gameObject.SetActive(true);
        next = false;
        yield return new WaitUntil(() => next == true);
        _dialogues.transform.GetChild(4).gameObject.SetActive(false);
        currentDialogue = 2;
        _dialogues.transform.GetChild(1).gameObject.SetActive(true);
        //parte audio
        yield return new WaitForSeconds(1);
        _dialogues.transform.GetChild(4).gameObject.SetActive(true);
        next = false;
        yield return new WaitUntil(() => next == true);
        _dialogues.transform.GetChild(4).gameObject.SetActive(false);
        currentDialogue = 3;
        _dialogues.transform.GetChild(2).gameObject.SetActive(true);
        //parte audio
        yield return new WaitForSeconds(1);
        _dialogues.transform.GetChild(5).gameObject.SetActive(true);
        next = false;
        TutorialHumanGenerator.readyToGenerate = true;
        yield return new WaitUntil(() => next == true);
        _dialogues.transform.GetChild(5).gameObject.SetActive(false);
        currentDialogue = 4;
        _dialogues.transform.GetChild(3).gameObject.SetActive(true);
        //parte audio
        yield return new WaitForSeconds(1);
        _dialogues.transform.GetChild(6).gameObject.SetActive(true);
    }
}
