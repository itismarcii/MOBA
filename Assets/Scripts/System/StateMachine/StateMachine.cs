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

        internal bool ChangeState(CharacterState state)
        {
            if(!TransitionAllowance(state)) return false;
            CurrentState = state;
            return true;
        }

        private bool TransitionAllowance(CharacterState state)
        {
            var condition = Conditions.FirstOrDefault(condition => condition.GetStateCondition() == state);
            return condition != null && condition.IsMet();
        }
    }
}
