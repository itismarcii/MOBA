using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace System.StateMachine
{
    [CreateAssetMenu(fileName = "State", menuName = "StateMachine")]
    public class State : ScriptableObject ,IState
    {
        [SerializeField] private List<StateTransition> Transitions = new List<StateTransition>();
        [SerializeField] private UnityEvent OnEnter = new UnityEvent();
        [SerializeField] private UnityEvent OnExit = new UnityEvent();

        public IState ProcessTransitions() => Transitions.FirstOrDefault(transition => transition.ShouldTransition)?.NextState;
        public State GetState() => this;

        public bool IsValid()
        {
            return Transitions.Count > 0;
        }
    }
}
