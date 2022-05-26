using UnityEngine;

namespace System.StateMachine
{
    [CreateAssetMenu(fileName = "Idle", menuName = "Condition/Idle")]
    public class IdleCondition : Condition
    {
        private void Awake()
        {
            StateCondition = CharacterState._Idle_;
        }

        internal override bool IsMet()
        {
            return true;
        }
    }
}
