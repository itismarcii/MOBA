using System;
using System.StateMachine;
using UnityEngine;
using BaseClass;

public static class Movement
{
    // WITHOUT PATHFINDING -- YET

    private static float _Tolerance = .1f;
    
    public static bool MoveTowards(Character character, Vector3 targetPosition)
    {
        if (character.GetCurrentState() != CharacterState._Walk_ &&
            character.GetCurrentState() != CharacterState._Slowed_) return false;
        
        var agent = character.GetAgent();
        var movementSpeed = character.GetMovementSpeed();
        if (movementSpeed < 0) movementSpeed = 0;

        agent.speed = movementSpeed;
        agent.SetDestination(targetPosition);
        
        return agent.hasPath;
    }
    
    public static bool MoveTowards(Spell spell, Vector3 targetPosition)
    {
        if (spell.GetTravellingRange() - spell.GetTravelledDistance() < _Tolerance)
        {
            spell.ActivateEffect();
            spell.Destroy();
        }
        
        var travellingSpeed = spell.GetTravellingSpeed();
        if (travellingSpeed < 0) travellingSpeed = 0;

        var transform = spell.transform;
        transform.LookAt(targetPosition);

        var position = transform.position;
        
        spell.transform.position = Vector3.MoveTowards(position, targetPosition, 
            travellingSpeed * Time.fixedDeltaTime);

        spell.SetTravelledDistance(spell.GetTravelledDistance() + travellingSpeed * Time.fixedDeltaTime);
        
        return Vector3.Distance(position, targetPosition) > _Tolerance;
    }

    public static void BlinkTowards(Character character, Vector3 targetPosition) =>
      character.transform.position = targetPosition;
}
