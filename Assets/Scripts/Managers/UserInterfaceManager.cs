using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager instance;
    [SerializeField] private TMP_Text _healthPoints;
    [SerializeField] private TMP_Text _level;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
}
