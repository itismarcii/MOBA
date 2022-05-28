using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace System.SkillMachine
{
    [Serializable]
    public class SkillTree
    {
        [Serializable]
        private class Branch
        {
            [SerializeField] internal Skill[] SkillBranch;
            [SerializeField] internal Branch[] NewBranch;

            internal Skill[] SearchBranch(Skill[] addTo, Branch branch)
            {
                var skillList = addTo.ToList();

                if (branch.NewBranch.Length > 0)
                {
                    foreach (var newBranch in branch.NewBranch)
                    {
                        var newSkills = SearchBranch(skillList.ToArray(), newBranch);
                        foreach (var skill in newSkills) skillList.Add(skill);
                    }
                }

                foreach (var skill in branch.SkillBranch) skillList.Add(skill);
                
                return skillList.ToArray();
            }
        }

        [SerializeField] private Skill[] Skills;
        private Skill[] ActiveSkills;
        
        [Tooltip("The Tree has one or several Branches. Each Branch will allow to only unlock the Skill if the" +
                 " previous Skill in order was already unlocked. If all Skills got unlocked, it is possible to skill" +
                 " further in NewBranches acting as regular Branches")]
        [SerializeField] private Branch[] Tree;


        internal Skill[] GetAllSkills()
        {
            if (Skills.Length > 0) return Skills;
            
            var allSkills = new List<Skill>();
            foreach (var branch in Tree)
            {
                var branchSkills = branch.SearchBranch(allSkills.ToArray(), branch);
                foreach (var skill in branchSkills) allSkills.Add(skill);
            }

            Skills = allSkills.ToArray();
            return Skills;

        }
        internal void UnlockSkill(Skill skill)
        {
            var selectedSkill = Skills.FirstOrDefault(s => s == skill);
            
            if (selectedSkill == null) return;
            
            selectedSkill.Unlock();
            
            if (!selectedSkill.CheckActive()) return;
            
            var newArray = ActiveSkills.ToList();
            
            newArray.Add(selectedSkill);
            ActiveSkills = newArray.ToArray();
        }
    }
}
