using BaseClass;
using UnityEngine;

namespace System.StateMachine
{
    public class WalkCondition : Condition
    {
        private readonly bool IsCastAndWalk;
        
        internal override bool IsMet()
        {
            return Character.GetCurrentState() != CharacterState._Death_ &&
                   Character.GetCurrentState() != CharacterState._Slowed_ &&
                   Character.GetCurrentState() != CharacterState._Stunned_ &&
                   Character.GetCurrentState() != CharacterState._Charmed_ &&
                   (IsCastAndWalk || Character.GetCurrentState() != CharacterState._CastAbility_);
        }
        
        public WalkCondition(Character character, CharacterState stateCondition, bool isCastAndWalk) : base(character, stateCondition)
        {
            IsCastAndWalk = isCastAndWalk;
        }
    }
}
