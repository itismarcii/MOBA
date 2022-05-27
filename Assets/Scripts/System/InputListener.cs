using System;
using System.Linq;
using System.StateMachine;
using BaseClass;
using UnityEngine;
using UnityEngine.AI;

public class InputListener : MonoBehaviour
{
    private Character[] AllCharacter;
    private Character Character;
    private Camera MainCamera;
    
    private bool MoveCommand = false;
    private Vector3 TargetPosition;


    // Abilites
    [SerializeField] private KeyCode FirstAbility;
    [SerializeField] private KeyCode SecondAbility;
    [SerializeField] private KeyCode ThirdAbility;
    [SerializeField] private KeyCode UltimateAbility;

    
    enum AbilityEnum
    {
        _First_,
        _Second_,
        _Third_,
        _Ultimate_
    }

    private void Start()
    {
        Character = transform.GetComponent<Character>();
        MainCamera = Camera.main;

        AllCharacter = FindObjectsOfType<Character>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) RightClick();
        if(Input.GetKeyDown(FirstAbility)) Ability(AbilityEnum._First_);
        if(Input.GetKeyDown(SecondAbility)) Ability(AbilityEnum._Second_);
        if(Input.GetKeyDown(ThirdAbility)) Ability(AbilityEnum._Third_);
        if(Input.GetKeyDown(UltimateAbility)) Ability(AbilityEnum._Ultimate_);
    }

    private void RightClick()
    {
        if(!Character.ChangeCurrentState(CharacterState._Walk_)) return;
        var ray = MainCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out var hit)) return;

        switch (hit.transform.tag)
        {
            case "Character":
                // Auto-attack
                break;
            default:
                MoveCommand = true;
                TargetPosition = hit.point;
                break;
        }
    }
    
    private void FixedUpdate()
    {
        if (!MoveCommand)
        {
            if ((Character.GetCurrentState() == CharacterState._Walk_ ||
                 Character.GetCurrentState() == CharacterState._Slowed_))
            {
                if (Character.GetAgent().speed == 0) Character.GetAgent().speed = Character.GetMovementSpeed();
                if (!Character.GetAgent().hasPath) Character.ChangeCurrentState(CharacterState._Idle_);
                return;
            }

            Character.GetAgent().speed = 0;
            return;
        }
        if (!Movement.MoveTowards(Character, TargetPosition)) return;
        MoveCommand = false;
    }

    void Ability(AbilityEnum ability)
    {
        var target = SelectTarget();
        Character character = null;

        if (target.transform.gameObject)
        {
            character = AllCharacter.FirstOrDefault(c => target.transform.gameObject == c.gameObject);
        }
        
        switch (ability)
        {
            case AbilityEnum._First_:
                if (character) Character.ActivateFirstAbility(character);
                else Character.ActivateFirstAbility(target.point);
                break;
            case AbilityEnum._Second_:
                if (character) Character.ActivateSecondAbility(character);
                else Character.ActivateSecondAbility(target.point);
                break;
            case AbilityEnum._Third_:
                if (character) Character.ActivateThirdAbility(character);
                else Character.ActivateThirdAbility(target.point);
                break;
            case AbilityEnum._Ultimate_:
                if (character) Character.ActivateUltimateAbility(character);
                else Character.ActivateUltimateAbility(target.point);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(ability), ability, null);
        }
    }

    RaycastHit SelectTarget()
    {
        var ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out var hit);
        return hit;
    }
}
