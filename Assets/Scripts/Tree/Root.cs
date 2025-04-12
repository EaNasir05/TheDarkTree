using UnityEngine;
using UnityEngine.Events;

public class Root : MonoBehaviour
{
    [SerializeField] private Transform _trapPlacement;
    [SerializeField] private GameObject[] _trapsPrefabs;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _standardSprite;
    [SerializeField] private Sprite _interactableSprite;
    private GameObject trap;
    private bool interactable;

    private void Awake()
    {
        _spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        interactable = false;
    }

    private void Update()
    {
        if (interactable && !GameManager.pause && !GameManager.selectingTrap)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (CorpseManager.instance.GetCorpse() != null)
                {
                    Cursor.visible = true;
                    GameManager.instance.FeedRoot();
                    CorpseManager.instance.DeleteCorpse();
                    if (trap != null)
                    {
                        _spriteRenderer.sprite = _standardSprite;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (trap == null && CorpseManager.instance.GetCorpse() == null)
                {
                    GameManager.instance.RootInteraction(gameObject);
                }
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
        _spriteRenderer.sprite = _standardSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactable = true;
            if (CorpseManager.instance.GetCorpse() != null || trap == null)
            {
                _spriteRenderer.sprite = _interactableSprite;
                if (GameManager.tutorial)
                {
                    GameManager.instance.RootTutorialCheck();
                }
            }
            else
            {
                _spriteRenderer.sprite = _standardSprite;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactable = false;
            _spriteRenderer.sprite = _standardSprite;
        }
    }
}
