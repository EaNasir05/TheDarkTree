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
    private float flowerAttackCooldown;
    private float sloughAttackCooldown;
    public bool insideFlower;
    public bool insideSlough;

    public int GetDamage() { return damage; }
    public float GetMovementSpeed() { return movementSpeed; }
    public float GetAttackCooldown() { return attackCooldown; }
    public float GetRange() { return range; }
    public int GetPower() { return power; }
    public Transform GetTree() { return _tree; }

    private void Start()
    {
        //GetComponent<StateMachine>().Initialise();
        /*flowerAttackCooldown = 0;
        sloughAttackCooldown = 0;
        insideFlower = false;
        insideSlough = false;*/
    }

    private void Update()
    {
        /*if (insideFlower)
        {
            flowerAttackCooldown += Time.deltaTime;
            if (flowerAttackCooldown >= 1)
            {
                DecreaseHealth(2);
                flowerAttackCooldown = 0;
            }
        }
        if (insideSlough)
        {
            sloughAttackCooldown += Time.deltaTime;
            if (sloughAttackCooldown >= 1)
            {
                DecreaseHealth(1);
                sloughAttackCooldown = 0;
            }
        }*/
    }

    public void ChangeMovementSpeed(float value)
    {
        movementSpeed += value;
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
