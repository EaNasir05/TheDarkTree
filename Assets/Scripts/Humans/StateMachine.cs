using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State activeState;
    private bool stunned;

    public void SetStunned(bool value) { stunned = value; }

    private void Awake()
    {
        stunned = false;
    }

    public void Initialise()
    {
        ChangeState(new MovementState());
    }

    private void FixedUpdate()
    {
        if (activeState != null && !stunned && !GameManager.pause)
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
