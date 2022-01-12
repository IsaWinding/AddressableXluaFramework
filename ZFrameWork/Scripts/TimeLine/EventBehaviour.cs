using EventG;

public enum BattleEventType
{ 
    Damg = 1,
    Other = 2,
}
//伤害类型

public enum DmageType { 
    Phy = 0,//物理伤害
    Magic = 1,
    Cure = 2,
}
//区域选择
public enum AreaType { 
    Sphere =0,//球形区域
    
}
//目标的阵容选择
public enum TargetSelectType
{
    Self = 0,
    Friend = 1,
    Enemy =2,
}
//伤害计算的类型
public enum DamgeValueType { 
    Single = 0,//只计算配置伤害
    AddPhyAtk = 1,//配置伤害加上单位的物理攻击伤害*影响系数
    
}

[System.Serializable]
public class DamgeEvent {
    public DmageType dmgeType = DmageType.Phy;
    public AreaType areaType = AreaType.Sphere;
    public TargetSelectType targetType = TargetSelectType.Self;
    public DamgeValueType damgeValueType = DamgeValueType.Single;
    public float range;//伤害的范围
    public float value;//伤害的配置数值
    public float effect;//影响系数

    public int owerId;
}

public class EventBehaviour : BaseBehaviour
{
    public DamgeEvent dmageEvent;
    public BattleEventType EventKey;
   
    private bool isCanSend = false;


    protected override void OnProgress(float pProgress)
    {
        if (pProgress >= 0.5f)
        {
            if (isCanSend)
            {
                var unitTag = target_.GetComponentInParent<UnitTag>();
                if (unitTag != null)
                {
                    dmageEvent.owerId = unitTag.Id;
                    BattleEnumEvent.SendMessage(EventKey, dmageEvent);
                }
                isCanSend = false;
            }
        }
        if (pProgress < 0.5f)
        {
            isCanSend = true;
        }
    }
}
