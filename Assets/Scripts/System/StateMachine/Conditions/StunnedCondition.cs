using BaseClass;
using UnityEngine;

namespace System.StateMachine
{
    public class StunnedCondition : Condition
    {
        internal override bool IsMet()
        {
            return true;
        }

        public StunnedCondition(Character character, CharacterState stateCondition) : base(character, stateCondition)
        {
        }
    }
}
