using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State activeState;

    public void Initialise()
    {
        //ChangeState(new PatrolState());
    }

    private void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(State newState)
    {
        if (activeState != null)
        {
            activeState.Exit();
        }
        activeState = newState;
        if (activeState != null)
        {
            activeState.stateMachine = this;
            //activeState.knight = GetComponent<Knight>();
            activeState.Enter();
        }
    }
}
