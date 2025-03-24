using UnityEngine;

public class DeadState : State
{
    private float timer;
    private bool expiring;

    public override void Enter()
    {
        timer = 0;
    }

    public override void Perform()
    {
        timer += Time.deltaTime;
        if (timer >= 20 && !expiring) {
            //animazione scomparsa
            expiring = true;
        }
        if(timer >= 25)
        {
            GameObject.Destroy(human);
        }
    }
}
