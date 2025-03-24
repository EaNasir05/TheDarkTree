using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State activeState;

    public void Initialise()
    {
        ChangeState(new MovementState());
    }

    private void FixedUpdate()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(State newState)
    {
        activeState = newState;
        if (activeState != null)
        {
            activeState.stateMachine = this;
            activeState.human = GetComponent<Human>();
            activeState.Enter();
        }
    }
}
