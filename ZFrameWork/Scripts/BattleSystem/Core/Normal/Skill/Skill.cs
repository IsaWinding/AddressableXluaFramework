using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Skill {

    public class Skill
    {
        public int Id;
        public float Cost;
        public float Cd;
        public string SkillTimeLineRes;//����չʾ���ܵ�TimeLine������Դ

        private Unit bindUnit;
        private float NextReleaseTime;
        public void BindUnit(Unit pUnit) {
            bindUnit = pUnit;
        }
        public bool IsCanRelease() {
            var curTime = Time.realtimeSinceStartup;
            if (curTime < NextReleaseTime) {
                return false;
            }
            if (bindUnit.mp < Cost) {
                return false;

            }
            return true;
        }
        public void OnRealse() {
            NextReleaseTime = Time.realtimeSinceStartup + Cd;
            bindUnit.OnCostMp(Cost);
        }
    }
}

