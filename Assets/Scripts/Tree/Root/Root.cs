using UnityEngine;
using UnityEngine.Events;

public class Root : MonoBehaviour
{
    [SerializeField] private Transform _trapPlacement;
    private GameObject trap;
    private bool interactable;

    private void Start()
    {
        interactable = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactable)
        {
            if (CorpseManager.instance.GetCorpse() != null)
            {
                GameManager.instance.FeedRoot();
                CorpseManager.instance.DeleteCorpse();
            }
            else if (trap == null)
            {
                GameManager.instance.RootInteraction(gameObject);
            }
        }
    }

    public void PlaceTrap(int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("FIORE PUTRIDO");
                break;
            case 1:
                Debug.Log("ROVI SANGUINARI");
                break;
            case 2:
                Debug.Log("MELMA OSCURA");
                break;
            case 3:
                Debug.Log("BULBO ESPLOSIVO");
                break;
            default:
                Debug.Log("CHE CAZZO HAI FATTO?");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactable = true;
            /*
            if (CorpseManager.instance.GetCorpse() != null || trap == null)
            {
                //cambia sprite
            }
            */
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactable = false;
            //cambia sprite
        }
    }
}
