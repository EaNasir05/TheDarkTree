using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State activeState;
    private bool stunned;
    private float stunTimer;

    public void SetStunned(bool value) { stunned = value; }

    private void Awake()
    {
        stunned = false;
        stunTimer = 0;
    }

    public void Initialise()
    {
        ChangeState(new MovementState());
    }

    private void Update()
    {
        if (stunned)
        {
            stunTimer += Time.deltaTime;
            if (stunTimer >= 2)
            {
                stunned = false;
                stunTimer = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!stunned && !GameManager.pause)
        {
            activeState.Perform();
        }
        else
        {
            if (activeState is MovementState)
            {
                (activeState as MovementState).Stop();
            }
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
