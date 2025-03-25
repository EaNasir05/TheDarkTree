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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _trapsList.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            _trapsList.transform.GetChild(1).GetComponent<Image>().color = Color.gray;
            _trapsList.transform.GetChild(2).GetComponent<Image>().color = Color.gray;
            _trapsList.transform.GetChild(3).GetComponent<Image>().color = Color.gray;
            _trapsList.transform.GetChild(4).GetComponent<Image>().color = Color.gray;
            _trapsList.transform.GetChild(5).GetComponent<Image>().color = Color.gray;
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
