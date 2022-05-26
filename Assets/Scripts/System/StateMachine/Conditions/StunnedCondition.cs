using UnityEngine;

namespace System.StateMachine
{
    [CreateAssetMenu(fileName = "Stunned", menuName = "Condition/Stunned")]
    public class StunnedCondition : Condition
    {
        private void Awake()
        {
            StateCondition = CharacterState._Stunned_;
        }
        
        internal override bool IsMet()
        {
            return true;
        }
    }
}
