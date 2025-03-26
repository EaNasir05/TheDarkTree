using UnityEngine;

public class DeadState : State
{
    private float timer;
    private bool expiring;

    public override void Enter()
    {
        human.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        timer = 0;
        expiring = false;
    }

    public override void Perform()
    {
        timer += Time.deltaTime;
        if (timer >= 15 && !expiring) {
            //animazione scomparsa
            expiring = true;
        }
        if(timer >= 20)
        {
            GameObject.Destroy(human.gameObject);
        }
    }
}
