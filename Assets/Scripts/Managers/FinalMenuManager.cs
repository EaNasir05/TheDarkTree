using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalMenuManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
