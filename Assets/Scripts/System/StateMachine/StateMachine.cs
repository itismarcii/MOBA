using System.Collections;
using System.Data;
using System.Linq;
using System.StateMachine.Conditions;
using BaseClass;
using UnityEngine;

namespace System.StateMachine
{
    public class StateMachine : MagicBehaviour
    {
        private Character Character;
        [SerializeField, Space(10)] private CharacterState CurrentState; 
        private Condition[] Conditions;
        private IEnumerator CurrentCoroutine;
        private CharacterState? PastState;
        
        public override void _Awake_()
        {
            Character = GetComponent<Character>();
            Conditions = new Condition[] { 
                new IdleCondition(Character, CharacterState._Idle_), 
                new WalkCondition(Character, CharacterState._Walk_, false),
                new CastAbilityCondition(Character, CharacterState._CastAbility_),
                new StunnedCondition(Character, CharacterState._Stunned_),
                new DeathCondition(Character, CharacterState._Death_)
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
            if(CurrentCoroutine != null) StopCoroutine(CurrentCoroutine);
            CurrentCoroutine = StateDuration_(state, duration);
            StartCoroutine(CurrentCoroutine);
            return true;
        }

        IEnumerator StateDuration_(CharacterState state, float duration)
        {
            if(CurrentState != state) PastState = CurrentState;
            CurrentState = state;
            yield return new WaitForSeconds(duration);
            if(PastState != null) CurrentState = (CharacterState) PastState;
        }

        internal bool TransitionAllowance(CharacterState state)
        {
            var condition = Conditions.FirstOrDefault(condition => condition.GetStateCondition() == state);
            return condition != null && condition.IsMet();
        }

    }
}
