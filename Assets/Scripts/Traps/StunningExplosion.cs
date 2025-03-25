using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StunningExplosion : MonoBehaviour
{
    private IEnumerator coroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            coroutine = StunHuman(collision.gameObject.GetComponent<StateMachine>());
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator StunHuman(StateMachine human)
    {
        human.SetStunned(true);
        yield return new WaitForSeconds(2);
        human.SetStunned(false);
    }
}
