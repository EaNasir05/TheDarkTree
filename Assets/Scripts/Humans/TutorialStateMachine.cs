using UnityEngine;

public class TutorialStateMachine : MonoBehaviour
{
    public TutorialState activeState;
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
        ChangeState(new TutorialMovementState());
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
            if (activeState is TutorialMovementState)
            {
                (activeState as TutorialMovementState).Stop();
            }
        }
    }

    public void ChangeState(TutorialState newState)
    {
        activeState = newState;
        if (activeState != null)
        {
            activeState.stateMachine = this;
            activeState.human = GetComponent<TutorialHuman>();
            activeState.Enter();
        }
    }
}
