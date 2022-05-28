using BaseClass;

namespace System.StateMachine.Conditions
{
    public class IdleCondition : Condition
    {
        internal override bool IsMet()
        {
            return Character.GetCurrentState() != CharacterState._Death_ &&
                   Character.GetCurrentState() != CharacterState._Slowed_ &&
                   Character.GetCurrentState() != CharacterState._Stunned_ &&
                   Character.GetCurrentState() != CharacterState._Charmed_;
        }

        public IdleCondition(Character character, CharacterState stateCondition) : base(character, stateCondition)
        {
        }
    }
}
