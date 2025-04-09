using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalMenuManager : MonoBehaviour
{
    private void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
