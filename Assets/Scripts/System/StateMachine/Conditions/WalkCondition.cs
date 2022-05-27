using BaseClass;
using UnityEngine;

namespace System.StateMachine
{
    public class WalkCondition : Condition
    {
        
        internal override bool IsMet()
        {
            return Character.GetCurrentState() != CharacterState._Death_ &&
                   Character.GetCurrentState() != CharacterState._Slowed_ &&
                   Character.GetCurrentState() != CharacterState._Stunned_ &&
                   Character.GetCurrentState() != CharacterState._Charmed_;
        }

        public WalkCondition(Character character, CharacterState stateCondition) : base(character, stateCondition)
        {
        }
    }
}
