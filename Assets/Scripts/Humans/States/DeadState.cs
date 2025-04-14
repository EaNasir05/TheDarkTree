using UnityEngine;

public class DeadState : State
{
    private SpriteRenderer _spriteRenderer;
    private float _expireTime;
    private float timer;

    public override void Enter()
    {
        human.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        _spriteRenderer = human.GetComponent<SpriteRenderer>();
        _expireTime = 15;
        timer = 0;
    }

    public override void Perform()
    {
        timer += Time.deltaTime;
        if (timer >= (_expireTime - 2.5) && timer < (_expireTime - 2)) {
            _spriteRenderer.color = new Color(1, 1, 1, (float)0.5);
        }
        if (timer >= (_expireTime - 2) && timer < (_expireTime - 1.5))
        {
            _spriteRenderer.color = new Color(1, 1, 1, 1);
        }
        if (timer >= (_expireTime - 1.5) && timer < (_expireTime - 1))
        {
            _spriteRenderer.color = new Color(1, 1, 1, (float)0.5);
        }
        if (timer >= (_expireTime - 1) && timer < (_expireTime - 0.5))
        {
            _spriteRenderer.color = new Color(1, 1, 1, 1);
        }
        if (timer >= (_expireTime - 0.5) && timer < _expireTime)
        {
            _spriteRenderer.color = new Color(1, 1, 1, (float)0.5);
        }
        if (timer >= _expireTime)
        {
            if (human.gameObject == CorpseManager.instance.GetCorpse())
            {
                Cursor.visible = true;
            }
            GameObject.Destroy(human.gameObject);
        }
    }
}
