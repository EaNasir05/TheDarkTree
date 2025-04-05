using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialBox;

    public void NewGame()
    {
        _tutorialBox.SetActive(true);
    }

    public void Settings()
    {
        //apre il menù di impostazioni
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetTutorial(bool tutorial)
    {
        if(tutorial)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            SceneManager.LoadScene("BattlegroundNoTutorial");
        }
    }
}
