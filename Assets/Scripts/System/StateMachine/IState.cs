namespace System.StateMachine
{
    public interface IState
    {
        IState ProcessTransitions();
        State GetState();
    }
}
