using System.Collections;
using System.Collections.Generic;
using System.StateMachine;
using BaseClass;
using UnityEngine;

public class CastAbilityCondition : Condition
{
    public CastAbilityCondition(Character character, CharacterState stateCondition) : base(character, stateCondition)
    {
    }

    internal override bool IsMet()
    {
        return true;
    }
}
