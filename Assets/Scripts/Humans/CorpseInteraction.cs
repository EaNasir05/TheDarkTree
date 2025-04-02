using UnityEngine;

public class CorpseInteraction : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Color _interactableColor;
    private bool interactable;

    private void Awake()
    {
        _interactableColor = Color.yellow;
    }

    private void Start()
    {
        _spriteRenderer.color = Color.white;
        interactable = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactable && !GameManager.pause && !GameManager.selectingTrap)
        {
            bool interacted = CorpseManager.instance.SetCorpse(gameObject);
            if (interacted)
            {
                _spriteRenderer.color = Color.white;
                gameObject.GetComponent<Collider2D>().enabled = false;
                interactable = false;
                Cursor.visible = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && CorpseManager.instance.GetCorpse() == null)
        {
            interactable = true;
            _spriteRenderer.color = _interactableColor;
        }
        else
        {
            interactable = false;
            _spriteRenderer.color = Color.white;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactable = false;
            _spriteRenderer.color = Color.white;
        }
    }
}
