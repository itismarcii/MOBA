using BaseClass;

namespace System.StateMachine
{
    public class DeathCondition : Condition
    {
        public DeathCondition(Character character, CharacterState stateCondition) : base(character, stateCondition)
        {
        }

        internal override bool IsMet()
        {
            return Character.GetHealth() <= 0;
        }
    }
}
