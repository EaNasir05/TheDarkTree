using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Brambles : MonoBehaviour
{
    private bool playerInBrambles = false;
    private List<Human> humansInBramble = new List<Human>();
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInBrambles = true;
            _spriteRenderer.color = new Color((float)0.1, (float)0.374, (float)0.141);
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
            _spriteRenderer.color = new Color((float)0.115, (float)0.306, (float)0.147);
            foreach (Human human in humansInBramble)
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
