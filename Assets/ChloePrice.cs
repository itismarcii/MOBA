using System;
using System.StateMachine;
using BaseClass;
using UnityEditor;
using UnityEngine;

public class ChloePrice : Character
{
    [SerializeField] private Transform SpellCastPosition;
    [SerializeField] private Spell FirstAbility;
    #region CustomEditor

    [CustomEditor(typeof(ChloePrice))]
    private class EditorClass : Editor
    {
        [SerializeField] private Character Character;

        private void OnEnable()
        {
            Character = (ChloePrice) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Space(20);
            
            if (GUILayout.Button("Revive"))
            {
                Character.Revive(new Vector3(0, 1, 0));
            }
        }
    }

    #endregion


    public override void _Start_()
    {
        SetMaxHealth(100);
        SetMovementSpeed(1);
        ChangeCurrentState(CharacterState._Stunned_, 3);
        SetWalkAndCast(false);
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
        ChangeCurrentState(CharacterState._CastAbility_, fireball.GetCastingSpeed());
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
