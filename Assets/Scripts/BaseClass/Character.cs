using System;
using System.Linq;
using System.StateMachine;
using UnityEngine;
using UnityEngine.AI;


namespace BaseClass
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody), typeof(StateMachine))]
    public abstract class Character : MonoBehaviour
    {
        public enum Team
        {
            _Team1_,
            _Team2_
        }

        private StateMachine StateMachine;
        
        private string CharacterName = "";
        [SerializeField] private Team TeamEnum;

        private bool IsAlive = true;
    
        private int Health;
        private int Armor;
        private int MagicResist;

        private int AttackPower;
        private int MagicPower;
        
        private int CoolDown;
        private int AttackSpeed;
        private int CritChance;
        private float MovementSpeed;
        private int LifeSteal;
        private Element[] Elements;

        private int Level;

        private Effect Passive;
        private Effect[] Abilities;
        private Spell[] Skills;

        private NavMeshAgent Agent;

        private void Awake()
        {
            StateMachine = GetComponent<StateMachine>();
            GetComponent<Rigidbody>().isKinematic = true;
            Agent = GetComponent<NavMeshAgent>();
            Agent.angularSpeed = 1000;
            Agent.acceleration = 200;

            transform.tag = "Character";
        }

        #region Getter & Setter

        // GETTER
        internal CharacterState GetCurrentState() => StateMachine.GetCurrentState();
        internal string GetCharacterName() => CharacterName;
        internal Team GetTeam() => TeamEnum;
        internal bool GetIsAlive() => IsAlive;
        internal int GetHealth() => Health;
        internal int GetArmor() => Armor;
        internal int GetMagicResist() => MagicResist;
        internal int GetAttackPower() => AttackPower;
        internal int GetMagicPower() => MagicPower;
        internal int GetCoolDown() => CoolDown;
        internal int GetAttackSpeed() => AttackSpeed;
        internal int GetCritChange() => CritChance;
        internal float GetMovementSpeed() => MovementSpeed;
        internal int GetLifeSteal() => LifeSteal;
        internal Element[] GetElements() => Elements;
        internal int GetLevel() => Level;
        internal Effect GetPassive() => Passive;
        internal Effect[] GetAbilities() => Abilities;
        internal Spell[] GetSkills() => Skills;
        internal NavMeshAgent GetAgent() => Agent;

        // SETTER
        internal bool ChangeCurrentState(CharacterState state) => StateMachine.ChangeState(state);
        internal void SetCharacterName(string characterName) => CharacterName = characterName;
        internal void SetTeam(Team team) => TeamEnum = team;
        internal void SetIsAlive(bool alive) => IsAlive = alive;
        internal void SetHealth(int health) => Health = health;
        internal void SetArmor(int armor) => Armor = armor;
        internal void SetMagicResist(int magicResist) => MagicResist = magicResist;
        internal void SetAttackPower(int attackPower) => AttackPower = attackPower;
        internal void SetMagicPower(int magicPower) => MagicPower = magicPower;
        internal void SetCoolDown(int coolDown) => CoolDown = coolDown;
        internal void SetAttackSpeed(int attackSpeed) => AttackSpeed = attackSpeed;
        internal void SetCritChance(int critChance) => CritChance = critChance;
        internal void SetMovementSpeed(float movementSpeed) => MovementSpeed = movementSpeed;
        internal void SetLifeSteal(int lifeSteal) => LifeSteal = lifeSteal;
        internal void SetElements(Element[] elements) => Elements = Elements;
        internal void SetElement(Element element)
        {
            var elements = Elements.ToList();
            elements.Add(element);
            Elements = elements.ToArray();
        } 
        internal void RemoveElement(Element element)
        {
            var elements = Elements.ToList();
            elements.Remove(element);
            Elements = elements.ToArray();
        }
        internal void SetLevel(int level) => Level = level;
        internal void SetPassive(Effect passive) => Passive = passive;
        internal void SetAbilities(Effect[] abilities) => Abilities = abilities;
        internal void SetSkills(Spell[] skills) => Skills = skills;
        internal void SetSkill(Spell effect)
        {
            var skills = Skills.ToList();
            skills.Add(effect);
            Skills = skills.ToArray();
        }
        internal void RemoveSkill(Spell effect)
        {
            var skills = Skills.ToList();
            skills.Remove(effect);
            Skills = skills.ToArray();
        }

        #endregion

        #region Abilities & Skills

        internal abstract void NormalAttack(Character target);
        internal abstract void MagicAttack(Character target);
        
        internal abstract void ActivatePassive(Character target);
        internal abstract void ActivateFirstAbility(Character target);
        internal abstract void ActivateFirstAbility(Vector3 target);
        internal abstract void ActivateFirstAbility(Building target);
        internal abstract void ActivateSecondAbility(Character target);
        internal abstract void ActivateSecondAbility(Vector3 target);
        internal abstract void ActivateSecondAbility(Building target);
        internal abstract void ActivateThirdAbility(Character target);
        internal abstract void ActivateThirdAbility(Vector3 target);
        internal abstract void ActivateThirdAbility(Building target);
        internal abstract void ActivateUltimateAbility(Character target);
        internal abstract void ActivateUltimateAbility(Vector3 target);
        internal abstract void ActivateUltimateAbility(Building target);

        #endregion

        #region System

        private void ReadInformation(TextAsset characterJson)
        {
            var character = JsonUtility.FromJson<Character>(characterJson.text);

            CharacterName = character.CharacterName;

            IsAlive = character.IsAlive;

            Health = character.Health;
            Armor = character.Armor;
            MagicResist = character.MagicResist;

            AttackPower = character.AttackPower;
            MagicPower = character.MagicPower;

            CoolDown = character.CoolDown;
            AttackSpeed = character.AttackSpeed;
            CritChance = character.CritChance;
            MovementSpeed = character.MovementSpeed;
            LifeSteal = character.LifeSteal;
            Elements = character.Elements;

            Level = character.Level;

            Passive = character.Passive;
            Abilities = character.Abilities;
            Skills = character.Skills;
        }

        #endregion
    }
}
