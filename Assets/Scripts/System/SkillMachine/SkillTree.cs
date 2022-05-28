using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace System.SkillMachine
{
    [Serializable]
    public class SkillTree
    {
        [Serializable]
        private class Branch
        {
            private static uint _Amount = 0;
            private uint ID;
            [SerializeField] internal Skill[] SkillBranch;
            [SerializeField] internal Branch[] NewBranch;

            internal void GiveID(Skill skill, uint id)
            {
                skill.ID = new uint[] {ID, id};
            }

            private Branch()
            {
                ID = _Amount;
                _Amount++;
            }

            internal Skill[] SearchBranch(IEnumerable<Skill> addTo, Branch branch)
            {
                var skillList = addTo.ToList();

                if (branch.NewBranch.Length > 0)
                {
                    foreach (var newBranch in branch.NewBranch)
                    {
                        var newSkills = SearchBranch(skillList.ToArray(), newBranch);
                        skillList.AddRange(newSkills);
                    }
                }

                skillList.AddRange(branch.SkillBranch);

                return skillList.ToArray();
            }
        }

        private Skill[] Skills;
        private Skill[] ActiveSkills;
        private Skill[] PassiveSkills;

        [Tooltip("The Tree has one or several Branches. Each Branch will allow to only unlock the Skill if the" +
                 " previous Skill in order was already unlocked. If all Skills got unlocked, it is possible to skill" +
                 " further in NewBranches acting as regular Branches")]
        [SerializeField]
        private Branch[] Tree;


        internal void ConfigSkills()
        {
            var allSkills = new List<Skill>();
            foreach (var branch in Tree)
            {
                var branchSkills = branch.SearchBranch(allSkills.ToArray(), branch);

                for (uint i = 0; i < branchSkills.Length; i++)
                {
                    branch.GiveID(branchSkills[i], i);
                    allSkills.Add(branchSkills[i]);
                }
            }

            Skills = allSkills.OrderByDescending(skill => skill.ID).ToArray();
        }

        internal void UnlockSkill(uint[] id)
        {
            var selectedSkill = SelectSkill(id);
            
            if (selectedSkill == null) return;
            
            selectedSkill.Unlock();
            
            if (!selectedSkill.CheckActive()) return;
            
            var newArray = ActiveSkills.ToList();

            if (selectedSkill.IsPassive)
            {
                var passiveList = PassiveSkills.ToList();
                passiveList.Add(selectedSkill);
                PassiveSkills = passiveList.ToArray();
            }
            
            newArray.Add(selectedSkill);
            ActiveSkills = newArray.ToArray();
        }

        internal Skill SelectSkill(uint[] id) => Skills.FirstOrDefault(skill => skill.ID == id);

        internal uint[] GetSkillID(Skill skill)
        {
            skill = Skills.FirstOrDefault(s => s == skill);
            return skill == null ? null : skill.ID;
        }

        internal void RunPassiveSkills()
        {
            if(PassiveSkills == null) return;
            foreach (var skill in PassiveSkills) skill.RunEffect();
        }
    }
}
