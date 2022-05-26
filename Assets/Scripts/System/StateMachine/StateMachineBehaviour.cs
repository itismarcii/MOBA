using System.Linq;
using UnityEngine;

namespace System.StateMachine
{
    public class StateMachineBehaviour : MonoBehaviour
    {
        [SerializeField] private State startingState = null;
        [SerializeField] private State[] States;
        private StateMachine StateMachine_;

        private void OnEnable()
        {
            var state = States.FirstOrDefault(state => state.IsValid());
            if (state != null) startingState = state;
        }

        private StateMachine StateMachine
        {
            get
            {
                if (StateMachine_ != null) return StateMachine_;
                StateMachine_ = new StateMachine(startingState);
                return StateMachine_;
            }
        }

        private void Update() => StateMachine.Tick();
        public void ChangeState(State state) => StateMachine.ChangeState(state);
        public State GetCurrentState() => StateMachine.GetCurrentState();
    }
}
