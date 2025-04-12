using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private int heath;
    [SerializeField] private int damage;
    [SerializeField] private float _maxMovementSpeed;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int power;
    [SerializeField] private Transform _tree;
    [SerializeField] private float avoidanceDistance;
    [SerializeField] private int level;
    [SerializeField] private Sprite _corpseSprite;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private float movementSpeed;
    private float bramblesAttackCooldown;
    private bool insideBrambles;
    private bool damaged;
    private float damagedSpriteCooldown;

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
    
    public void DecreaseMovementSpeed()
    {
        movementSpeed = (float)(_maxMovementSpeed * 0.66);
    }

    public void IncreaseMovementSpeed()
    {
        movementSpeed = _maxMovementSpeed;
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = Color.white;
        GetComponent<StateMachine>().Initialise();
        movementSpeed = _maxMovementSpeed;
        bramblesAttackCooldown = 0;
        damagedSpriteCooldown = 0;
        insideBrambles = false;
    }

    private void Update()
    {
        if (insideBrambles && !GameManager.pause)
        {
            bramblesAttackCooldown += Time.deltaTime;
            if (bramblesAttackCooldown >= 0.5)
            {
                DecreaseHealth(1);
                bramblesAttackCooldown = 0;
            }
        }
        if (damaged)
        {
            damagedSpriteCooldown += Time.deltaTime;
            if (damagedSpriteCooldown >= 0.15)
            {
                _spriteRenderer.color = Color.white;
                damagedSpriteCooldown = 0;
                damaged = false;
            }
        }
    }

    public void DecreaseHealth(int damage)
    {
        heath -= damage;
        _spriteRenderer.color = Color.red;
        damaged = true;
        if (heath <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<StateMachine>().ChangeState(new DeadState());
        _spriteRenderer.sprite = _corpseSprite;
        gameObject.tag = "Corpse";
        _collider.isTrigger = true;
        _collider.offset = new Vector2(0, (float)-0.15);
        _collider.size = new Vector2((float)1.75, (float)1.75);
        gameObject.GetComponent<CorpseInteraction>().enabled = true;
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        gameObject.layer = LayerMask.NameToLayer("Corpse");
        _spriteRenderer.color = Color.white;
        this.enabled = false;
    }
}
