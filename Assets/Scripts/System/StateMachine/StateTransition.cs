using System.Collections.Generic;
using UnityEngine;

namespace System.StateMachine
{
    [Serializable]
    public class StateTransition
    {
        [SerializeField] private State nextState;
        [SerializeField] private List<ICondition> Conditions = new List<ICondition>();
        public State NextState => nextState;
        public bool ShouldTransition => ConditionHandler.AreMet(Conditions);
    }
}
