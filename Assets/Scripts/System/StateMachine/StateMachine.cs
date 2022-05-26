namespace System.StateMachine
{
    public class StateMachine
    {
        private IState CurrentState { get; set; }
        public State GetCurrentState() => CurrentState.GetState();

        protected internal StateMachine(IState startingState) => ChangeState(startingState);

        public void ChangeState(IState state)
        {
            CurrentState = state;
        }

        public void Tick()
        {
            var nextState = CurrentState.ProcessTransitions();
        
            if(nextState != null) ChangeState(nextState);
        }
    }
}
