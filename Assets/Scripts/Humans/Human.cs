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
    [SerializeField] private Transform _tree;
    [SerializeField] private float avoidanceDistance;
    [SerializeField] private int level;
    private float bramblesAttackCooldown;
    private bool insideBrambles;

    public int GetDamage() { return damage; }
    public float GetMovementSpeed() { return movementSpeed; }
    public float GetAttackCooldown() { return attackCooldown; }
    public float GetRange() { return range; }
    public int GetPower() { return power; }
    public Transform GetTree() { return _tree; }
    public void SetTree(Transform tree) { _tree = tree; }
    public float GetAvoidenceDistance() { return avoidanceDistance; }
    public int GetLevel() { return level; }
    public void SetInsideBrambles(bool value) { insideBrambles = value; }
    
    public void HalveMovementSpeed()
    {
        movementSpeed /= 2;
    }

    public void DoubleMovementSpeed()
    {
        movementSpeed *= 2;
    }

    private void Start()
    {
        GetComponent<StateMachine>().Initialise();
        bramblesAttackCooldown = 0;
        insideBrambles = false;
    }

    private void Update()
    {
        if (insideBrambles && !GameManager.pause)
        {
            bramblesAttackCooldown += Time.deltaTime;
            if (bramblesAttackCooldown >= 1)
            {
                DecreaseHealth(1);
                bramblesAttackCooldown = 0;
            }
        }
    }

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
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        gameObject.layer = LayerMask.NameToLayer("Corpse");
        this.enabled = false;
    }
}
