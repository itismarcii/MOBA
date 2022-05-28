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

        private StateMachine StateMachine;
        [SerializeField, Space(10)] private SkillTree SkillTree = new SkillTree();
        
        private string CharacterName = "";
        [SerializeField] private Team TeamEnum;

        private bool IsAlive = true;

        private int MaxHealth;
        [SerializeField] private int Health;
        private int Armor;
        private int MagicResist;

        private int AttackPower;
        private int MagicPower;
        
        private int CoolDown;
        private int AttackSpeed;
        private int CriticalChance;
        private float MovementSpeed;
        private int LifeSteal;
        private Element[] Elements;

        private int Level;

        private Effect Passive;
        private Effect[] Abilities;
        private Spell[] Skills;

        private NavMeshAgent Agent;
        private Rigidbody Rigidbody;
        private Collider Collider;

        void Awake()
        {
            LoadingPriority = 0;
        }
        
        public override void _Awake_()
        {
            StateMachine = GetComponent<StateMachine>();
            Rigidbody = GetComponent<Rigidbody>();
            Collider = GetComponent<Collider>();
            Rigidbody.isKinematic = true;
            Agent = GetComponent<NavMeshAgent>();
            Agent.angularSpeed = 1000;
            Agent.acceleration = 200;

            transform.tag = "Character";
            SkillTree.ConfigSkills();
        }

        public override void _Update_()
        {
            base._Update_();
            if (!CanTransitionTo(CharacterState._Death_)) return;
            ChangeCurrentState(CharacterState._Death_);
            Rigidbody.Sleep();
            Agent.enabled = false;
            Collider.enabled = false;
        }


        #region Getter & Setter

        // GETTER
        internal CharacterState GetCurrentState() => StateMachine.GetCurrentState();
        internal SkillTree GetSkillTree() => SkillTree;
        internal string GetCharacterName() => CharacterName;
        internal Team GetTeam() => TeamEnum;
        internal bool GetIsAlive() => IsAlive;
        internal int GetMaxHealth() => MaxHealth;
        internal int GetHealth() => Health;
        internal int GetArmor() => Armor;
        internal int GetMagicResist() => MagicResist;
        internal int GetAttackPower() => AttackPower;
        internal int GetMagicPower() => MagicPower;
        internal int GetCoolDown() => CoolDown;
        internal int GetAttackSpeed() => AttackSpeed;
        internal int GetCritChange() => CriticalChance;
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
        internal bool CanTransitionTo(CharacterState state) => StateMachine.TransitionAllowance(state);
        internal bool ChangeCurrentState(CharacterState state, float duration) => StateMachine.ChangeState(state, duration);
        internal void SetCharacterName(string characterName) => CharacterName = characterName;
        internal void SetTeam(Team team) => TeamEnum = team;
        internal void SetIsAlive(bool alive) => IsAlive = alive;
        internal void SetMaxHealth(int health) => MaxHealth = health;
        internal void SetHealth(int health) => Health = health;
        internal void SetArmor(int armor) => Armor = armor;
        internal void SetMagicResist(int magicResist) => MagicResist = magicResist;
        internal void SetAttackPower(int attackPower) => AttackPower = attackPower;
        internal void SetMagicPower(int magicPower) => MagicPower = magicPower;
        internal void SetCoolDown(int coolDown) => CoolDown = coolDown;
        internal void SetAttackSpeed(int attackSpeed) => AttackSpeed = attackSpeed;
        internal void SetCriticalChance(int criticalChance) => CriticalChance = criticalChance;
        internal void SetMovementSpeed(float movementSpeed) => MovementSpeed = movementSpeed;
        internal void SetLifeSteal(int lifeSteal) => LifeSteal = lifeSteal;
        internal void SetElements(Element[] elements) => Elements = elements;
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

        internal void SetWalkAndCast(bool state) => StateMachine.SetCastAndWalk(state);

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
            ChangeCurrentState(CharacterState._Idle_);
            SetHealth(GetMaxHealth());
            transform.position = position;
            Agent.enabled = true;
            Collider.enabled = true;
            Agent.SetDestination(position);
        }

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
            CriticalChance = character.CriticalChance;
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
