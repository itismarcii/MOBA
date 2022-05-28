using System.Linq;
using UnityEngine;

namespace System.SkillMachine
{
    [Serializable]
    public class SkillTree
    {
        [Serializable]
        private struct Branch
        {
            [SerializeField] internal Skill[] SkillBranch;
            [SerializeField] internal Branch[] NewBranch;
        }

        [SerializeField] private Skill[] Skills;
        private Skill[] ActiveSkills;
        
        [Tooltip("The Tree has one or several Branches. Each Branch will allow to only unlock the Skill if the" +
                 " previous Skill in order was already unlocked. If all Skills got unlocked, it is possible to skill" +
                 " further in NewBranches acting as regular Branches")]
        [SerializeField] private Branch[] Tree;

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
