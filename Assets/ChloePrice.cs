using System;
using System.StateMachine;
using BaseClass;
using UnityEngine;

public class ChloePrice : Character
{
    [SerializeField] private Transform SpellCastPosition;
    [SerializeField] private Spell FirstAbility;
    
    private void Start()
    {
        SetMovementSpeed(1);
        ChangeCurrentState(CharacterState._Stunned_, 3);
    }

    internal override void NormalAttack(Character target)
    {
        throw new System.NotImplementedException();
    }

    internal override void MagicAttack(Character target)
    {
        throw new System.NotImplementedException();
    }

    internal override void ActivatePassive(Character target)
    {
        throw new System.NotImplementedException();
    }

    internal override void ActivateFirstAbility(Character target)
    {
        ActivateFirstAbility(target.transform.position);
    }

    internal override void ActivateFirstAbility(Vector3 target)
    {
        transform.LookAt(target);
        
        var fireball = Instantiate(FirstAbility);
        fireball.SetCaster(this);
        fireball.transform.position = SpellCastPosition.transform.position;
        target = new Vector3(target.x, SpellCastPosition.position.y, target.z);
        fireball.MoveTowards(target);
    }

    internal override void ActivateFirstAbility(Building target)
    {
        throw new System.NotImplementedException();
    }

    internal override void ActivateSecondAbility(Character target)
    {
        throw new System.NotImplementedException();
    }

    internal override void ActivateSecondAbility(Vector3 target)
    {
        throw new System.NotImplementedException();
    }

    internal override void ActivateSecondAbility(Building target)
    {
        throw new System.NotImplementedException();
    }

    internal override void ActivateThirdAbility(Character target)
    {
        throw new System.NotImplementedException();
    }

    internal override void ActivateThirdAbility(Vector3 target)
    {
        throw new System.NotImplementedException();
    }

    internal override void ActivateThirdAbility(Building target)
    {
        throw new System.NotImplementedException();
    }

    internal override void ActivateUltimateAbility(Character target)
    {
        throw new System.NotImplementedException();
    }

    internal override void ActivateUltimateAbility(Vector3 target)
    {
        throw new System.NotImplementedException();
    }

    internal override void ActivateUltimateAbility(Building target)
    {
        throw new System.NotImplementedException();
    }
}
