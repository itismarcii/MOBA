using BaseClass;
using UnityEngine;

namespace System.StateMachine
{
    public class IdleCondition : Condition
    {
        internal override bool IsMet()
        {
            return true;
        }

        public IdleCondition(Character character, CharacterState stateCondition) : base(character, stateCondition)
        {
        }
    }
}
