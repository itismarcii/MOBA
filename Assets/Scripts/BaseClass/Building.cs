using System;

namespace BaseClass
{
    public abstract class Building : MagicBehaviour
    {
        private string BuildingName = "";
        private int CoolDown;
        
        private int Health;
        private int Armor;
        private int MagicResist;
        
        private int AttackPower;
        private int MagicPower;
        
        #region Getter & Setter
        
        internal string GetBuildingName() => BuildingName;
        internal int GetCoolDown() => CoolDown;
        internal int GetHealth() => Health;
        internal int GetArmor() => Armor;
        internal int GetMagicResist() => MagicResist;
        internal int GetAttackPower() => AttackPower;
        internal int GetMagicPower() => MagicPower;

        internal void SetBuildingName(string effectName) => BuildingName = effectName;
        internal void SetCoolDown(int coolDown) => CoolDown = coolDown;
        internal void SetHealth(int health) => Health = health;
        internal void SetArmor(int armor) => Armor = armor;
        internal void SetMagicResist(int magicResist) => MagicResist = magicResist;
        internal void SetAttackPower(int attackPower) => AttackPower = attackPower;
        internal void SetMagicPower(int magicPower) => MagicPower = magicPower;

        #endregion
    }
}
