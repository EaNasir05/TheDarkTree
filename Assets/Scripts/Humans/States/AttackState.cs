using UnityEngine;

public class AttackState : State
{
    private float cooldown;

    public override void Enter()
    {
        cooldown = human.GetAttackCooldown();
    }

    public override void Perform()
    {
        cooldown += Time.deltaTime;
        if (cooldown >= human.GetAttackCooldown())
        {
            cooldown = 0;
            //animazione attacco
            UserInterfaceManager.instance.DecreaseHP(human.GetDamage());
        }
    }
}
