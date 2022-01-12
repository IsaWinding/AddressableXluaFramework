using Battle.Skill;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Creat/Unit")]
public class UnitSetter : ScriptableObject
{

    public AttributeSetter attributeSetter;
    public StateSetter stateSetter;
    public AIPathSetter aiPathSetter;
    public List<SkillSetter> skillSetters;
    public AIPolicyType aiPolicyType;
    public Vector3 bornPos;

    public void SetByGO(GameObject pGO)
    {
        var pos_ = pGO.transform.position;
        pos_.y = 0;
        bornPos = pos_;
    }

    public static UnitSetter LoadDataFromFile(string Path){
        var textAsset = AssetManager.Instance.LoadAsset<UnitSetter>(Path);
        return textAsset;
    }
    public Unit GetUnit(int pId){
        var unit = new Unit(pId, attributeSetter.GetAttributeInfo(), stateSetter.GetStateInfo(bornPos));
        if (aiPathSetter != null){
            unit.LoadAiPath(aiPathSetter.GetAIPath());
        }
        unit.LoadAIPolicy(aiPolicyType);
        if (skillSetters != null){
            var skillInfo = new SkillInfo();
            foreach (var temp in skillSetters) {
                var skill = temp.GetSkill();
                skill.BindUnit(unit);
                skillInfo.AddSkill(skill);
            }
            unit.SetSkillInfo(skillInfo);
        }
        return unit;
    }
}
