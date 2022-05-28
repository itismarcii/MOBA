using BaseClass;

namespace System.StateMachine.Conditions
{
    public class StunnedCondition : Condition
    {
        internal override bool IsMet()
        {
            return Character.GetCurrentState() != CharacterState._Death_;
        }

        public StunnedCondition(Character character, CharacterState stateCondition) : base(character, stateCondition)
        {
        }
    }
}
