using UnityEngine;

public class TutorialDeadState : TutorialState
{
    private SpriteRenderer _spriteRenderer;

    public override void Enter()
    {
        human.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
    }

    public override void Perform()
    {
        
    }
}
