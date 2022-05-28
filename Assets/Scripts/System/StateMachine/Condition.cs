using BaseClass;

namespace System.StateMachine
{
    public abstract class Condition
    {
        internal Character Character;
        private readonly CharacterState StateCondition;


        protected Condition(Character character, CharacterState stateCondition)
        {
            Character = character;
            StateCondition = stateCondition;
        }

        internal abstract bool IsMet();
        internal CharacterState GetStateCondition() => StateCondition;
        internal void SetCharacter(Character character) => Character = character;
    }
}
