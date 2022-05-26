using System;
using BaseClass;
using UnityEngine;

public class FireBall : Spell
{
    internal override void StartSpell()
    {
        throw new NotImplementedException();
    }

    internal override void StartSpell(Character character)
    {
        throw new NotImplementedException();
    }

    internal override void StartSpell(Character[] characters)
    {
        throw new NotImplementedException();
    }

    internal override void StartSpell(Vector3 location)
    {
        MoveTowards(location);
    }

    internal override void StartSpell(Building building)
    {
        throw new NotImplementedException();
    }

    internal override void StartSpell(Building[] buildings)
    {
        throw new NotImplementedException();
    }

    internal override void ActivateEffect()
    {
        //GetEffect().ActivateEffect(GetAreaRange());
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Character":
                if(other.GetComponent<Character>().GetTeam() == GetCaster().GetTeam()) break;
                ActivateEffect();
                Destroy(gameObject);
                break;
        }
    }
}
