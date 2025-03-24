public abstract class State
{
    public StateMachine stateMachine;
    public Human human;

    public abstract void Enter();

    public abstract void Perform();
}
