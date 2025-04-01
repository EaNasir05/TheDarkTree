using UnityEngine;

public class DeadState : State
{
    private SpriteRenderer _spriteRenderer;
    private float timer;

    public override void Enter()
    {
        human.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        _spriteRenderer = human.GetComponent<SpriteRenderer>();
        timer = 0;
    }

    public override void Perform()
    {
        timer += Time.deltaTime;
        if (timer >= 17.5 && timer < 18) {
            _spriteRenderer.color = new Color(1, 1, 1, (float)0.5);
        }
        if (timer >= 18 && timer < 18.5)
        {
            _spriteRenderer.color = new Color(1, 1, 1, 1);
        }
        if (timer >= 18.5 && timer < 19)
        {
            _spriteRenderer.color = new Color(1, 1, 1, (float)0.5);
        }
        if (timer >= 19 && timer < 19.5)
        {
            _spriteRenderer.color = new Color(1, 1, 1, 1);
        }
        if (timer >= 19.5 && timer < 20)
        {
            _spriteRenderer.color = new Color(1, 1, 1, (float)0.5);
        }
        if (timer >= 20)
        {
            GameObject.Destroy(human.gameObject);
        }
    }
}
