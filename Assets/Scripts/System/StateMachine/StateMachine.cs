using System.Collections;
using System.Linq;
using BaseClass;
using UnityEngine;

namespace System.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        private Character Character;
        [SerializeField] private CharacterState CurrentState; 
        private Condition[] Conditions;
        
        private void Awake()
        {
            Character = GetComponent<Character>();
            Conditions = new Condition[] { 
                new IdleCondition(Character, CharacterState._Idle_), 
                new WalkCondition(Character, CharacterState._Walk_, false),
                new CastAbilityCondition(Character, CharacterState._CastAbility_),
                new StunnedCondition(Character, CharacterState._Stunned_) 
            };
        }

        internal CharacterState GetCurrentState() => CurrentState;

        internal void SetCastAndWalk(bool state)
        {
            for (var i = 0; i < Conditions.Length; i++)
            {
                if (Conditions[i].GetStateCondition() != CharacterState._Walk_) continue;
                var condition = new WalkCondition(Character, CharacterState._Walk_, state);
                Conditions[i] = condition;
            }
        }

        internal bool ChangeState(CharacterState state)
        {
            if(!TransitionAllowance(state)) return false;
            CurrentState = state;
            return true;
        }

        internal bool ChangeState(CharacterState state, float duration)
        {
            if(!TransitionAllowance(state)) return false;
            StartCoroutine(StateDuration_(state, duration));
            return true;
        }

        IEnumerator StateDuration_(CharacterState state, float duration)
        {
            var pastState = CurrentState;
            CurrentState = state;
            yield return new WaitForSeconds(duration);
            CurrentState = pastState;
        }

        private bool TransitionAllowance(CharacterState state)
        {
            var condition = Conditions.FirstOrDefault(condition => condition.GetStateCondition() == state);
            return condition != null && condition.IsMet();
        }
    }
}
