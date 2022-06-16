using System;
using System.Linq;
using System.SkillMachine;
using System.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace BaseClass
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody), typeof(CapsuleCollider))]
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(PhysicMaterial))]
    [RequireComponent(typeof(StateMachine), typeof(InputListener))]
    public abstract class Character : MagicBehaviour
    {
        public enum Team
        {
            _Team1_,
            _Team2_
        }
        
        [Serializable]
        internal struct Info
        {
            internal StateMachine StateMachine;
            [SerializeField, Space(10)] internal SkillTree SkillTree;
        
            internal string CharacterName;
            [SerializeField] internal Team TeamEnum;

            internal bool IsAlive;

            [SerializeField] internal int MaxHealth;
            [SerializeField] internal int Health;
            internal int Armor;
            internal int MagicResist;

            internal int AttackPower;
            internal int MagicPower;
        
            internal int CoolDown;
            internal int AttackSpeed;
            internal int CriticalChance;
            internal float MovementSpeed;
            internal int LifeSteal;
            internal Element[] Elements;

            internal int Level;

            internal Effect Passive;
            internal Effect[] Abilities;
            internal Spell[] Skills;

            internal NavMeshAgent Agent;
            internal Rigidbody Rigidbody;
            internal Collider Collider;
        }

        [SerializeField] private Info Information;
        

        void Awake()
        {
            LoadingPriority = 0;
        }
        
        public override void _Awake_()
        {
            Information.StateMachine = GetComponent<StateMachine>();
            Information.Rigidbody = GetComponent<Rigidbody>();
            Information.Collider = GetComponent<Collider>();
            Information.Rigidbody.isKinematic = true;
            Information.Agent = GetComponent<NavMeshAgent>();
            Information.Agent.angularSpeed = 1000;
            Information.Agent.acceleration = 200;

            transform.tag = "Character";
            Information.SkillTree.ConfigSkills();
        }

        public override void _Update_()
        {
            Information.SkillTree.RunPassiveSkills();
            CheckDeath();
        }

        private void CheckDeath()
        {
            if (!CanTransitionTo(CharacterState._Death_)) return;
            ChangeCurrentState(CharacterState._Death_);
            Information.Rigidbody.Sleep();
            Information.Agent.enabled = false;
            Information.Collider.enabled = false;
        }


        #region Getter & Setter

        // GETTER
        internal CharacterState GetCurrentState() => Information.StateMachine.GetCurrentState();
        internal SkillTree GetSkillTree() => Information.SkillTree;
        internal string GetCharacterName() => Information.CharacterName;
        internal Team GetTeam() => Information.TeamEnum;
        internal bool GetIsAlive() => Information.IsAlive;
        internal int GetMaxHealth() => Information.MaxHealth;
        internal int GetHealth() => Information.Health;
        internal int GetArmor() => Information.Armor;
        internal int GetMagicResist() => Information.MagicResist;
        internal int GetAttackPower() => Information.AttackPower;
        internal int GetMagicPower() => Information.MagicPower;
        internal int GetCoolDown() => Information.CoolDown;
        internal int GetAttackSpeed() => Information.AttackSpeed;
        internal int GetCritChange() => Information.CriticalChance;
        internal float GetMovementSpeed() => Information.MovementSpeed;
        internal int GetLifeSteal() => Information.LifeSteal;
        internal Element[] GetElements() => Information.Elements;
        internal int GetLevel() => Information.Level;
        internal Effect GetPassive() => Information.Passive;
        internal Effect[] GetAbilities() => Information.Abilities;
        internal Spell[] GetSkills() => Information.Skills;
        internal NavMeshAgent GetAgent() => Information.Agent;

        // SETTER
        internal bool ChangeCurrentState(CharacterState state) => Information.StateMachine.ChangeState(state);
        internal bool CanTransitionTo(CharacterState state) => Information.StateMachine.TransitionAllowance(state);
        internal bool ChangeCurrentState(CharacterState state, float duration) => Information.StateMachine.ChangeState(state, duration);
        internal void SetCharacterName(string characterName) => Information.CharacterName = characterName;
        internal void SetTeam(Team team) => Information.TeamEnum = team;
        internal void SetIsAlive(bool alive) => Information.IsAlive = alive;
        internal void SetMaxHealth(int health) => Information.MaxHealth = health;
        internal void SetHealth(int health) => Information.Health = health;
        internal void SetArmor(int armor) => Information.Armor = armor;
        internal void SetMagicResist(int magicResist) => Information.MagicResist = magicResist;
        internal void SetAttackPower(int attackPower) => Information.AttackPower = attackPower;
        internal void SetMagicPower(int magicPower) => Information.MagicPower = magicPower;
        internal void SetCoolDown(int coolDown) => Information.CoolDown = coolDown;
        internal void SetAttackSpeed(int attackSpeed) => Information.AttackSpeed = attackSpeed;
        internal void SetCriticalChance(int criticalChance) => Information.CriticalChance = criticalChance;
        internal void SetMovementSpeed(float movementSpeed) => Information.MovementSpeed = movementSpeed;
        internal void SetLifeSteal(int lifeSteal) => Information.LifeSteal = lifeSteal;
        internal void SetElements(Element[] elements) => Information.Elements = elements;
        internal void SetElement(Element element)
        {
            var elements = Information.Elements.ToList();
            elements.Add(element);
            Information.Elements = elements.ToArray();
        } 
        internal void RemoveElement(Element element)
        {
            var elements = Information.Elements.ToList();
            elements.Remove(element);
            Information.Elements = elements.ToArray();
        }
        internal void SetLevel(int level) => Information.Level = level;
        internal void SetPassive(Effect passive) => Information.Passive = passive;
        internal void SetAbilities(Effect[] abilities) => Information.Abilities = abilities;
        internal void SetSkills(Spell[] skills) => Information.Skills = skills;
        internal void SetSkill(Spell effect)
        {
            var skills = Information.Skills.ToList();
            skills.Add(effect);
            Information.Skills = skills.ToArray();
        }
        internal void RemoveSkill(Spell effect)
        {
            var skills = Information.Skills.ToList();
            skills.Remove(effect);
            Information.Skills = skills.ToArray();
        }

        internal void SetWalkAndCast(bool state) => Information.StateMachine.SetCastAndWalk(state);

        #endregion

        #region Abilities & Skills

        internal abstract void NormalAttack(Character target);
        internal abstract void MagicAttack(Character target);
        
        internal abstract void ActivatePassive(Character target);

        internal virtual void ActivateFirstAbility(Character target)
        {
            if(CanTransitionTo(CharacterState._CastAbility_)) return;
        }
        
        internal virtual void ActivateFirstAbility(Vector3 target)
        {
            if(CanTransitionTo(CharacterState._CastAbility_)) return;
        }
        
        internal virtual void ActivateFirstAbility(Building target)
        {
            if(CanTransitionTo(CharacterState._CastAbility_)) return;
        }
        
        internal virtual void ActivateSecondAbility(Character target)
        {
            if(CanTransitionTo(CharacterState._CastAbility_)) return;
        }
        
        internal virtual void ActivateSecondAbility(Vector3 target)
        {
            if(CanTransitionTo(CharacterState._CastAbility_)) return;
        }
        
        internal virtual void ActivateSecondAbility(Building target)
        {
            if(CanTransitionTo(CharacterState._CastAbility_)) return;
        }
        
        internal virtual void ActivateThirdAbility(Character target)
        {
            if(CanTransitionTo(CharacterState._CastAbility_)) return;
        }
        
        internal virtual void ActivateThirdAbility(Vector3 target)
        {
            if(CanTransitionTo(CharacterState._CastAbility_)) return;
        }
        
        internal virtual void ActivateThirdAbility(Building target)
        {
            if(CanTransitionTo(CharacterState._CastAbility_)) return;
        }
        
        internal virtual void ActivateUltimateAbility(Character target)
        {
            if(CanTransitionTo(CharacterState._CastAbility_)) return;
        }
        
        internal virtual void ActivateUltimateAbility(Vector3 target)
        {
            if(CanTransitionTo(CharacterState._CastAbility_)) return;
        }

        internal virtual void ActivateUltimateAbility(Building target)
        {
            if(CanTransitionTo(CharacterState._CastAbility_)) return;
        }
        
        #endregion

        #region System

        internal void Revive(Vector3 position)
        {
            ChangeCurrentState(CharacterState._Revive_);
            SetHealth(GetMaxHealth());
            transform.position = position;
            Information.Agent.enabled = true;
            Information.Collider.enabled = true;
            Information.Agent.SetDestination(position);
            ChangeCurrentState(CharacterState._Idle_);
        }

        private void ReadInformation(TextAsset characterJson)
        {
            var character = JsonUtility.FromJson<Character>(characterJson.text);

            Information.CharacterName = character.Information.CharacterName;

            Information.IsAlive = character.Information.IsAlive;

            Information.Health = character.Information.Health;
            Information.Armor = character.Information.Armor;
            Information.MagicResist = character.Information.MagicResist;

            Information.AttackPower = character.Information.AttackPower;
            Information.MagicPower = character.Information.MagicPower;

            Information.CoolDown = character.Information.CoolDown;
            Information.AttackSpeed = character.Information.AttackSpeed;
            Information.CriticalChance = character.Information.CriticalChance;
            Information.MovementSpeed = character.Information.MovementSpeed;
            Information.LifeSteal = character.Information.LifeSteal;
            Information.Elements = character.Information.Elements;

            Information.Level = character.Information.Level;

            Information.Passive = character.Information.Passive;
            Information.Abilities = character.Information.Abilities;
            Information.Skills = character.Information.Skills;
        }

        #endregion
    }
}
