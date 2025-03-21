using UnityEngine;
using UnityEngine.Events;

public class RootInteraction : MonoBehaviour
{
    private bool interactable;
    public UnityEvent interaction;

    private void Start()
    {
        interactable = false;
        interaction.AddListener(GameManager.instance.OnRootInteractionEvent);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactable)
        {
            interaction.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactable = false;
        }
    }
}
