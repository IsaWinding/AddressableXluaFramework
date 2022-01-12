using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Skill
{
    [CreateAssetMenu(menuName = "Creat/NewSkillSetter")]
    public class SkillSetter : ScriptableObject
    {
        public int Id;
        public float Cost;
        public float Cd;
        public string SkillTimeLineRes;//用来展示节能的TimeLine表现资源

        public static SkillSetter LoadDataFromFile(string Path)
        {
            var textAsset = AssetManager.Instance.LoadAsset<SkillSetter>(Path);
            return textAsset;
        }

        public Skill GetSkill(){
            var skill = new Skill() {
                Id = this.Id, Cost = this.Cost, Cd = this.Cd, SkillTimeLineRes = this.SkillTimeLineRes
            };
            return skill;
        }
    }
}

