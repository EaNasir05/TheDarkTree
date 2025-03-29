using UnityEngine;

public class AttackState : State
{
    private float cooldown;

    public override void Enter()
    {
        human.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        cooldown = human.GetAttackCooldown();
    }

    public override void Perform()
    {
        cooldown += Time.deltaTime;
        if (cooldown >= human.GetAttackCooldown())
        {
            cooldown = 0;
            //animazione attacco
            GameManager.instance.DecreaseHealthPoints(human.GetDamage());
        }
    }
}
