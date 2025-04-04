using UnityEngine;

abstract public class TutorialState
{
    public TutorialStateMachine stateMachine;
    public TutorialHuman human;

    public abstract void Enter();

    public abstract void Perform();
}
