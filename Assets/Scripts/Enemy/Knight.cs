using UnityEngine;

public class Knight : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private int heath;
    [SerializeField] private int damage;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float attackSpeed;

    public void DecreaseHealth(int damage)
    {
        heath -= damage;
        if (heath <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;
        gameObject.tag = "Corpse";
        _collider.isTrigger = true;
        _collider.size = new Vector2((float)1.5, (float)1.5);
        gameObject.GetComponent<CorpseInteraction>().enabled = true;
        this.enabled = false;
    }
}
