using BaseClass;
using UnityEngine;

namespace System.StateMachine
{
    public abstract class Condition : ScriptableObject
    {
        internal Character Character;
        internal CharacterState StateCondition;
        
        internal abstract bool IsMet();
        internal CharacterState GetStateCondition() => StateCondition;
        internal void SetCharacter(Character character) => Character = character;
    }
}
