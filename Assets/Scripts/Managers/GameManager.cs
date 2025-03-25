using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool pause;
    public static bool selectingTrap;
    private int level = 1;
    private int corpses = 0;
    [SerializeField] private GameObject _tree;
    [SerializeField] private int[] _milestones;
    private int[] trapsAmounts = {1, 0, 0, 0, 0, 0};
    private string[] _trapsNames = {
        "Runa Magica",
        "Rovi Sanguinari",
        "Melma Oscura",
        "Bulbo Esplosivo",
        "Bulbo Stordente",
        "Cristallo Arcano"
    };
    private string[] _trapsDescriptions = {
        "Finché lo stregone è nella runa, la sua frequenza di sparo è aumentata",
        "Se lo stregone ci capita sopra, tutti gli umani sui rovi prendono danni nel tempo",
        "Rallenta il movimento degli umani che ci capitano sopra",
        "Se colpito dalla palla di fuoco esplode, infliggendo danni agli umani vicini",
        "Se colpito dalla palla di fuoco esplode, stordendo gli umani vicini",
        "Se colpito dalla palla di fuoco, aumenta per pochi secondi il danno inflitto agli umani"
    };
    //private Sprite[] _trapsSprites = { };
    private GameObject currentRoot;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        pause = false;
        selectingTrap = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
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

    public void FeedRoot()
    {
        corpses++;
        if (_milestones[level-1] == corpses)
        {
            UserInterfaceManager.instance.IncreaseLevel();
            level++;
            corpses = 0;
        }
    }
}
