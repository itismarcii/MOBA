using UnityEngine;

namespace BaseClass
{
    
    public abstract class Effect : MonoBehaviour
    {
        private string EffectName = "";
        private int CoolDown;

        
        #region Getter & Setter
        
        internal string GetEffectName() => EffectName;
        internal int GetCoolDown() => CoolDown;

        internal void SetEffectName(string effectName) => EffectName = effectName;
        internal void SetCoolDown(int coolDown) => CoolDown = coolDown;

        #endregion

        internal abstract void ActivateEffect();
        internal abstract void ActivateEffect(Character target);
        internal abstract void ActivateEffect(Character[] targets);
        internal abstract void ActivateEffect(Vector2 areaForm);
        internal abstract void ActivateEffect(float areaRadius);
        internal abstract void ActivateEffect(Building building);
        internal abstract void ActivateEffect(Building[] buildings);
    }
}
