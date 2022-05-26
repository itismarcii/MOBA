using System.Linq;
using BaseClass;
using UnityEngine;

namespace System.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        private Character Character;
        [SerializeField] private CharacterState CurrentState; 
        [SerializeField] private Condition[] Conditions;
        
        private void Awake()
        {
            Character = GetComponent<Character>();
            Conditions = UnityEngine.Resources.LoadAll<Condition>("State");
            foreach (var condition in Conditions) condition.SetCharacter(Character);
        }

        internal CharacterState GetCurrentState() => CurrentState;

        internal void ChangeState(CharacterState state)
        {
            if(!TransitionAllowance(state)) return;
            CurrentState = state;
        
            switch (state)
            {
                case CharacterState._Idle_:
                    break;
                case CharacterState._Walk_:
                    break;
                case CharacterState._CastAbility_:
                    break;
                case CharacterState._Stunned_:
                    break;
                case CharacterState._Charmed_:
                    break;
                case CharacterState._Slowed_:
                    break;
                case CharacterState._Death_:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private bool TransitionAllowance(CharacterState state)
        {
            var condition = Conditions.FirstOrDefault(condition => condition.GetStateCondition() == state);
            return condition != null && condition.IsMet();
        }
    }
}
