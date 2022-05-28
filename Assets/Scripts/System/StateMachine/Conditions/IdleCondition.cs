using BaseClass;

namespace System.StateMachine.Conditions
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
