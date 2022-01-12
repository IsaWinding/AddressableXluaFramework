using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Skill
{
    public class SkillInfo
    {
        private Dictionary<int, Skill> allSkills = new Dictionary<int, Skill>();

        public void AddSkill(Skill pSkill) {
            allSkills.Add(pSkill.Id, pSkill);
        }
        public Skill GetSkill(int pSkillId) {
            Skill skill;
            allSkills.TryGetValue(pSkillId,out skill);
            return skill;
        }
        
    }
}

