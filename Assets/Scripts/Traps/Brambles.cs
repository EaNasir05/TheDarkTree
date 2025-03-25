using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Brambles : MonoBehaviour
{
    private bool playerInBrambles = false;
    private List<Human> humansInBramble = new List<Human>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInBrambles = true;
            foreach (Human human in humansInBramble)
            {
                human.SetInsideBrambles(true);
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Human human = collision.gameObject.GetComponent<Human>();
            humansInBramble.Add(human);
            if (playerInBrambles)
            {
                human.SetInsideBrambles(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInBrambles = false;
            foreach(Human human in humansInBramble)
            {
                human.SetInsideBrambles(false);
            }
        }
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            Human human = collision.gameObject.GetComponent<Human>();
            humansInBramble.Remove(human);
            human.SetInsideBrambles(false);
        }
    }
}
