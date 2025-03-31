using UnityEngine;

public class CorpseInteraction : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Color _interactableColor;
    private bool interactable;

    private void Awake()
    {
        _interactableColor = new Color((float)0.66, (float)0.29, (float)0.12);
    }

    private void Start()
    {
        _spriteRenderer.color = Color.white;
        interactable = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactable && !GameManager.pause)
        {
            CorpseManager.instance.SetCorpse(gameObject);
            if (CorpseManager.instance.GetCorpse() == gameObject)
            {
                gameObject.GetComponent<Collider2D>().enabled = false;
                interactable = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && CorpseManager.instance.GetCorpse() == null)
        {
            interactable = true;
            if(_spriteRenderer.color == Color.white)
            {
                _spriteRenderer.color = _interactableColor;
            }
        }
        else
        {
            interactable = false;
            if(_spriteRenderer.color == _interactableColor)
            {
                _spriteRenderer.color = Color.white;
            }
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
