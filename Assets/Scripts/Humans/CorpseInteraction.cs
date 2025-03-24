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
        if (Input.GetKeyDown(KeyCode.E) && interactable)
        {
            if (CorpseManager.instance.GetCorpse() == null)
            {
                Debug.Log("CADAVERE RACCATTATO");
                CorpseManager.instance.SetCorpse(gameObject);
                gameObject.GetComponent<Collider2D>().enabled = false;
                interactable = false;
            }
            else
            {
                Debug.Log("NON PUOI RACCATTARE ALTRI CADAVERI");
            }
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
