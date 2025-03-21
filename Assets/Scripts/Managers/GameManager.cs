using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int level = 1;
    private int corpses = 0;
    [SerializeField] private int[] milestones;
    private int[] trapsList = {1, 0, 0, 0};
    private GameObject currentRoot;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void RootInteraction(GameObject root)
    {
        currentRoot = root;
        UserInterfaceManager.instance.OpenTrapsList();
        Debug.Log("SCEGLI TRAPPOLA");
    }

    public void TrapSelection(int index)
    {
        if (trapsList[index] > 0)
        {
            currentRoot.GetComponent<Root>().PlaceTrap(index);
            trapsList[index]--;
            UserInterfaceManager.instance.CloseTrapsList();
            UserInterfaceManager.instance.UpdateTrapsList(index, trapsList[index]);
        }
        else
        {
            Debug.Log("NON HAI TRAPPOLE DI QUESTO TIPO");
        }
    }

    public void FeedRoot()
    {
        Debug.Log("SACRIFICIO EFFETTUATO");
        corpses++;
        if (milestones[level-1] == corpses)
        {
            UserInterfaceManager.instance.IncreaseLevel();
            level++;
            corpses = 0;
        }
    }
}
