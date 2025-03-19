public abstract class State
{
    public StateMachine stateMachine;
    //public Knight knight;

    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}
