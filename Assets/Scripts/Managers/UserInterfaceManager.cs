using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager instance;
    [SerializeField] private TMP_Text _healthPoints;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private GameObject _trapsList;
    [SerializeField] private GameObject _levelUpTraps;
    [SerializeField] private GameObject _levelUpEnhancements;
    [SerializeField] private SpriteRenderer _treeSpriteRenderer;
    private bool damaged;
    private float damagedSpriteTimer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _trapsList.transform.GetChild(0).GetComponent<Image>().color = Color.gray;
            _trapsList.transform.GetChild(1).GetComponent<Image>().color = Color.gray;
            _trapsList.transform.GetChild(2).GetComponent<Image>().color = Color.gray;
            _trapsList.transform.GetChild(3).GetComponent<Image>().color = Color.gray;
            _trapsList.transform.GetChild(4).GetComponent<Image>().color = Color.gray;
            _trapsList.transform.GetChild(5).GetComponent<Image>().color = Color.gray;
            damagedSpriteTimer = 0;
            damaged = false;
        }
    }

    private void Update()
    {
        if (damaged)
        {
            damagedSpriteTimer += Time.deltaTime;
            if(damagedSpriteTimer >= 0.15)
            {
                _treeSpriteRenderer.color = Color.white;
                damaged = false;
                damagedSpriteTimer = 0;
            }
        }
    }

    public void IncreaseLevel()
    {
        int currentLevel = int.Parse(_level.text);
        currentLevel++;
        _level.text = currentLevel.ToString();
    }

    public void DecreaseHP(int damage)
    {
        int currentHP = int.Parse(_healthPoints.text);
        currentHP -= damage;
        if (currentHP < 0)
        {
            currentHP = 0;
        }
        _treeSpriteRenderer.color = Color.red;
        damaged = true;
        _healthPoints.text = (currentHP).ToString();
    }

    public void OpenTrapsList()
    {
        _trapsList.SetActive(true);
    }

    public void CloseTrapsList()
    {
        _trapsList.SetActive(false);
    }

    public void GenerateLevelUpTraps(string name1, string name2, string description1, string description2)
    {
        _levelUpTraps.transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().text = name1;
        _levelUpTraps.transform.GetChild(1).transform.GetChild(2).GetComponent<TMP_Text>().text = description1;
        _levelUpTraps.transform.GetChild(2).transform.GetChild(0).GetComponent<TMP_Text>().text = name2;
        _levelUpTraps.transform.GetChild(2).transform.GetChild(2).GetComponent<TMP_Text>().text = description2;
    }

    public void GenerateLevelUpEnhancements(string name1, string name2, string description1, string description2)
    {
        _levelUpEnhancements.transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().text = name1;
        _levelUpEnhancements.transform.GetChild(1).transform.GetChild(2).GetComponent<TMP_Text>().text = description1;
        _levelUpEnhancements.transform.GetChild(2).transform.GetChild(0).GetComponent<TMP_Text>().text = name2;
        _levelUpEnhancements.transform.GetChild(2).transform.GetChild(2).GetComponent<TMP_Text>().text = description2;
    }

    public void OpenLevelUpTraps()
    {
        _levelUpTraps.SetActive(true);
    }

    public void CloseLevelUpTraps()
    {
        _levelUpTraps.SetActive(false);
    }

    public void OpenLevelUpEnhancements()
    {
        _levelUpEnhancements.SetActive(true);
    }

    public void CloseLevelUpEnhancements()
    {
        _levelUpEnhancements.SetActive(false);
    }

    public void UpdateTrapsList(int index, int amount)
    {
        _trapsList.transform.GetChild(index).transform.GetChild(1).GetComponent<TMP_Text>().text = amount.ToString();
        if (amount > 0)
        {
            _trapsList.transform.GetChild(index).GetComponent<Image>().color = Color.white;
        }
        else
        {
            _trapsList.transform.GetChild(index).GetComponent<Image>().color = Color.gray;
        }
    }
}
