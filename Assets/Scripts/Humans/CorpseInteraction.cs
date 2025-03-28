using UnityEngine;

public class CorpseInteraction : MonoBehaviour
{
    private bool interactable;

    private void Start()
    {
        interactable = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactable && !GameManager.pause)
        {
            CorpseManager.instance.SetCorpse(gameObject);
            gameObject.GetComponent<Collider2D>().enabled = false;
            interactable = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && CorpseManager.instance.GetCorpse() == null)
        {
            interactable = true;
            //cambia sprite
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
