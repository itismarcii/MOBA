using BaseClass;
using UnityEngine;

namespace System.StateMachine
{
    public class IdleCondition : Condition
    {
        private void Awake()
        {
            StateCondition = CharacterState._Idle_;
        }

        internal override bool IsMet()
        {
            return true;
        }

        public IdleCondition(Character character, CharacterState stateCondition) : base(character, stateCondition)
        {
        }
    }
}
