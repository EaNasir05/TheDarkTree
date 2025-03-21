using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int level = 1;
    private int[] milestones = { 10, 25, 50 };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void OnRootInteractionEvent()
    {
        UserInterfaceManager.instance.IncreaseLevel();
    }
}
