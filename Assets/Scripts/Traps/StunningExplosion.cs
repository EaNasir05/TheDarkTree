using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StunningExplosion : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<StateMachine>().SetStunned(true);
        }
    }
}
