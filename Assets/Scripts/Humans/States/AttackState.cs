using UnityEngine;

public class AttackState : State
{
    private float cooldown;
    private bool ranged;

    public override void Enter()
    {
        human.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        cooldown = human.GetAttackCooldown();
        if (human.GetRange() > 3)
        {
            ranged = true;
        }
        else
        {
            ranged = false;
        }
    }

    public override void Perform()
    {
        cooldown += Time.deltaTime;
        if (cooldown >= human.GetAttackCooldown())
        {
            cooldown = 0;
            //animazione attacco
            if (!ranged)
            {
                GameManager.instance.DecreaseHealthPoints(human.GetDamage());
            }
            else
            {
                GameObject.Instantiate(Resources.Load<GameObject>("Arrow"), human.transform.position, Quaternion.identity);
            }
        }
    }
}
