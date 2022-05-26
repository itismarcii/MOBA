using System;
using UnityEngine;
using BaseClass;
using UnityEngine.AI;

public static class Movement
{
    // WITHOUT PATHFINDING -- YET

    private static float _Tolerance = .1f;
    
    public static bool MoveTowards(Character character, Vector3 targetPosition)
    {
        var agent = character.GetAgent();
        var movementSpeed = character.GetMovementSpeed();
        if (movementSpeed < 0) movementSpeed = 0;

        agent.speed = movementSpeed;
        agent.SetDestination(targetPosition);
        
        return agent.isStopped;
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
