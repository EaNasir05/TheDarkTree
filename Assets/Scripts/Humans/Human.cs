using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private int heath;
    [SerializeField] private int damage;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int power;

    public int GetDamage() { return damage; }
    public float GetMovementSpeed() { return movementSpeed; }
    public float GetAttackCooldown() { return attackCooldown; }
    public float GetRange() { return range; }
    public int GetPower() { return power; }
    
    public void DecreaseHealth(int damage)
    {
        heath -= damage;
        if (heath <= 0)
        {
            Die();
            //animazione morte
        }
    }

    private void Die()
    {
        GetComponent<StateMachine>().ChangeState(new DeadState());
        GetComponent<SpriteRenderer>().color = Color.yellow;
        gameObject.tag = "Corpse";
        _collider.isTrigger = true;
        _collider.size = new Vector2((float)1.5, (float)1.5);
        gameObject.GetComponent<CorpseInteraction>().enabled = true;
        this.enabled = false;
    }
}
