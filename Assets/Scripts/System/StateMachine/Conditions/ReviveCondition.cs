using BaseClass;

namespace System.StateMachine.Conditions
{
    public class ReviveCondition : Condition
    {
        public ReviveCondition(Character character, CharacterState stateCondition) : base(character, stateCondition)
        {
        }

        internal override bool IsMet()
        {
            return Character.GetCurrentState() == CharacterState._Death_;
        }
    }
}
