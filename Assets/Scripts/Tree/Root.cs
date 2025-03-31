using UnityEngine;
using UnityEngine.Events;

public class Root : MonoBehaviour
{
    [SerializeField] private Transform _trapPlacement;
    [SerializeField] private GameObject[] _trapsPrefabs;
    private GameObject trap;
    private bool interactable;

    private void Start()
    {
        interactable = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && interactable && !GameManager.pause && !GameManager.selectingTrap)
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
                trap = Instantiate(_trapsPrefabs[0], _trapPlacement.position, Quaternion.identity);
                break;
            case 1:
                trap = Instantiate(_trapsPrefabs[1], _trapPlacement.position, Quaternion.identity);
                break;
            case 2:
                trap = Instantiate(_trapsPrefabs[2], _trapPlacement.position, Quaternion.identity);
                break;
            case 3:
                trap = Instantiate(_trapsPrefabs[3], _trapPlacement.position, Quaternion.identity);
                break;
            case 4:
                trap = Instantiate(_trapsPrefabs[4], _trapPlacement.position, Quaternion.identity);
                break;
            case 5:
                trap = Instantiate(_trapsPrefabs[5], _trapPlacement.position, Quaternion.identity);
                break;
            default:
                Debug.Log("TRAPPOLA INESISTENTE");
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
