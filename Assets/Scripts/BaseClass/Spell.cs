using System;
using System.Security.Cryptography;
using UnityEngine;

namespace BaseClass
{
    public abstract class Spell : MonoBehaviour
    {
        private Character Caster;
        [SerializeField] private int AreaRange;
        [SerializeField] private int TravellingRange;
        [SerializeField] private float TravellingSpeed;
        [SerializeField] private Effect Effect;

        private float TravelledDistance = 0;
        private bool IsTravelling = false;
        private bool IsUpdating = false;
        private Vector3 TargetVector3;
        private Character TargetCharacter;
        
        #region Getter & Setter

        internal Character GetCaster() => Caster;
        internal int GetAreaRange() => AreaRange;
        internal int GetTravellingRange() => TravellingRange;
        internal float GetTravellingSpeed() => TravellingSpeed;
        internal Effect GetEffect() => Effect;
        internal float GetTravelledDistance() => TravelledDistance;

        internal void SetCaster(Character caster) => Caster = caster; 
        internal void SetAreaRange(int range) => AreaRange = range;
        internal void SetTravellingRange(int range) => TravellingRange = range;
        internal void SetTravellingSpeed(float speed) => TravellingSpeed = speed;
        internal void SetEffect(Effect effect) => Effect = effect;
        internal void SetTravelledDistance(float distance) => TravelledDistance = distance; 

        #endregion

        internal abstract void StartSpell();
        internal abstract void StartSpell(Character character);
        internal abstract void StartSpell(Character[] characters);
        internal abstract void StartSpell(Vector3 location);
        internal abstract void StartSpell(Building building);
        internal abstract void StartSpell(Building[] buildings);

        private void Awake()
        {
            if (TravellingSpeed < 0) TravellingSpeed = 0;
            IsTravelling = false;
        }

        private void FixedUpdate()
        {
            if (!IsTravelling) return;
            if (IsUpdating)
            {
                if (Movement.MoveTowards(this, TargetCharacter.transform.position)) return;
            }
            
            if (Movement.MoveTowards(this, TargetVector3)) ;
        }
        
        internal void MoveTowards(Vector3 target)
        {
            TargetVector3 = target;
            IsTravelling = true;
        }

        internal void MoveTowards(Character character)
        {
            TargetCharacter = character;
            IsTravelling = true;
            IsUpdating = true;
        }

        internal abstract void ActivateEffect();

        internal void Destroy() => Destroy(gameObject); //CURRENTLY
        
        #region System

        private void ReadInformation(TextAsset characterJson)
        {
            var spell = JsonUtility.FromJson<Spell>(characterJson.text);

            AreaRange = spell.AreaRange;
            TravellingRange = spell.TravellingRange;
            TravellingSpeed = spell.TravellingSpeed;
            Effect = spell.Effect;
        }

        #endregion
    }
}
