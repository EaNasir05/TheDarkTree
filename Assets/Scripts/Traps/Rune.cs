using UnityEngine;

public class Rune : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Shooting>().ChangeFireballCooldown((float)-0.2);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Shooting>().ChangeFireballCooldown((float)0.2);
        }
    }
}
