using UnityEngine;

public class MovementState : State
{
    public override void Enter()
    {
        Debug.Log("MOVEMENT STATE");
    }

    public override void Perform()
    {
        //animazione corsa
    }
}
