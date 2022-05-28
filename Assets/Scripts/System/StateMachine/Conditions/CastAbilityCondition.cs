using BaseClass;

namespace System.StateMachine.Conditions
{
    public class CastAbilityCondition : Condition
    {
    
        public CastAbilityCondition(Character character, CharacterState stateCondition) : base(character, stateCondition)
        {
        }

        internal override bool IsMet()
        {
            return Character.GetCurrentState() != CharacterState._Death_ &&
                   Character.GetCurrentState() != CharacterState._Slowed_ &&
                   Character.GetCurrentState() != CharacterState._Stunned_ &&
                   Character.GetCurrentState() != CharacterState._Charmed_;
        
        }

    
    }
}
