using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager instance;
    [SerializeField] private TMP_Text _healthPoints;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private Slider _corpsesCount;
    [SerializeField] private TMP_Text _trapsCount;
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
        }
        damagedSpriteTimer = 0;
        damaged = false;
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

    public void ResetCorpsesCount()
    {
        _corpsesCount.value = 0;
    }

    public void IncreaseCorpsesCount()
    {
        _corpsesCount.value++;
    }

    public void SetCorpsesMaxValue(int value)
    {
        _corpsesCount.maxValue = value;
    }

    public void IncreaseTrapsCount()
    {
        int count = int.Parse(_trapsCount.text);
        count++;
        _trapsCount.text = count.ToString();
    }

    public void DecreaseTrapsCount()
    {
        int count = int.Parse(_trapsCount.text);
        count--;
        _trapsCount.text = count.ToString();
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

    public void ShowHP()
    {
        _healthPoints.transform.parent.gameObject.SetActive(true);
    }

    public void HideHP()
    {
        _healthPoints.transform.parent.gameObject.SetActive(false);
    }

    public void ShowLevel()
    {
        _level.transform.parent.gameObject.SetActive(true);
    }

    public void HideLevel()
    {
        _level.transform.parent.gameObject.SetActive(false);
    }

    public void OpenTrapsList()
    {
        Cursor.visible = true;
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
        RectTransform rect = _levelUpTraps.transform.GetChild(1).transform.GetChild(2).GetComponent<RectTransform>();
        switch (name1)
        {
            case "RUNA MAGICA":
                rect.sizeDelta = new Vector2(500, rect.sizeDelta.y);
                break;
            case "BULBO ESPLOSIVO":
                rect.sizeDelta = new Vector2(500, rect.sizeDelta.y);
                break;
            case "BULBO STORDENTE":
                rect.sizeDelta = new Vector2(500, rect.sizeDelta.y);
                break;
            case "CRISTALLO ARCANO":
                rect.sizeDelta = new Vector2(475, rect.sizeDelta.y);
                break;
            default:
                rect.sizeDelta = new Vector2(520, rect.sizeDelta.y);
                break;
        }
        _levelUpTraps.transform.GetChild(2).transform.GetChild(0).GetComponent<TMP_Text>().text = name2;
        _levelUpTraps.transform.GetChild(2).transform.GetChild(2).GetComponent<TMP_Text>().text = description2;
        rect = _levelUpTraps.transform.GetChild(2).transform.GetChild(2).GetComponent<RectTransform>();
        switch (name2)
        {
            case "RUNA MAGICA":
                rect.sizeDelta = new Vector2(500, rect.sizeDelta.y);
                break;
            case "BULBO ESPLOSIVO":
                rect.sizeDelta = new Vector2(500, rect.sizeDelta.y);
                break;
            case "BULBO STORDENTE":
                rect.sizeDelta = new Vector2(500, rect.sizeDelta.y);
                break;
            case "CRISTALLO ARCANO":
                rect.sizeDelta = new Vector2(475, rect.sizeDelta.y);
                break;
            default:
                rect.sizeDelta = new Vector2(520, rect.sizeDelta.y);
                break;
        }
    }

    public void GenerateLevelUpEnhancements(string name1, string name2, string description1, string description2)
    {
        _levelUpEnhancements.transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().text = name1;
        _levelUpEnhancements.transform.GetChild(1).transform.GetChild(2).GetComponent<TMP_Text>().text = description1;
        RectTransform rect = _levelUpEnhancements.transform.GetChild(1).transform.GetChild(2).GetComponent<RectTransform>();
        switch (name1)
        {
            case "DEVASTAZIONE":
                rect.sizeDelta = new Vector2(400, rect.sizeDelta.y);
                break;
            case "PIEDE LESTO":
                rect.sizeDelta = new Vector2(370, rect.sizeDelta.y);
                break;
            default:
                rect.sizeDelta = new Vector2(430, rect.sizeDelta.y);
                break;
        }
        _levelUpEnhancements.transform.GetChild(2).transform.GetChild(0).GetComponent<TMP_Text>().text = name2;
        _levelUpEnhancements.transform.GetChild(2).transform.GetChild(2).GetComponent<TMP_Text>().text = description2;
        rect = _levelUpEnhancements.transform.GetChild(2).transform.GetChild(2).GetComponent<RectTransform>();
        switch (name2)
        {
            case "DEVASTAZIONE":
                rect.sizeDelta = new Vector2(400, rect.sizeDelta.y);
                break;
            case "PIEDE LESTO":
                rect.sizeDelta = new Vector2(370, rect.sizeDelta.y);
                break;
            default:
                rect.sizeDelta = new Vector2(430, rect.sizeDelta.y);
                break;
        }
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
        _trapsList.transform.GetChild(0).transform.GetChild(index).transform.GetChild(1).GetComponent<TMP_Text>().text = amount.ToString();
        if (amount > 0)
        {
            _trapsList.transform.GetChild(0).transform.GetChild(index).GetComponent<Button>().interactable = true;
        }
        else
        {
            _trapsList.transform.GetChild(0).transform.GetChild(index).GetComponent<Button>().interactable = false;
        }
    }
}
