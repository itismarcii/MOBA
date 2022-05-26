using UnityEngine;

namespace System.StateMachine
{
    [CreateAssetMenu(fileName = "Walk", menuName = "Condition/Walk")]
    public class WalkCondition : Condition
    {
        
        private void Awake()
        {
            StateCondition = CharacterState._Walk_;
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
