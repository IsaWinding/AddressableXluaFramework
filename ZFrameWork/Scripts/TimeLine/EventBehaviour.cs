using EventG;

public enum BattleEventType
{ 
    Damg = 1,
    Other = 2,
}
//�˺�����

public enum DmageType { 
    Phy = 0,//�����˺�
    Magic = 1,
    Cure = 2,
}
//����ѡ��
public enum AreaType { 
    Sphere =0,//��������
    
}
//Ŀ�������ѡ��
public enum TargetSelectType
{
    Self = 0,
    Friend = 1,
    Enemy =2,
}
//�˺����������
public enum DamgeValueType { 
    Single = 0,//ֻ���������˺�
    AddPhyAtk = 1,//�����˺����ϵ�λ���������˺�*Ӱ��ϵ��
    
}

[System.Serializable]
public class DamgeEvent {
    public DmageType dmgeType = DmageType.Phy;
    public AreaType areaType = AreaType.Sphere;
    public TargetSelectType targetType = TargetSelectType.Self;
    public DamgeValueType damgeValueType = DamgeValueType.Single;
    public float range;//�˺��ķ�Χ
    public float value;//�˺���������ֵ
    public float effect;//Ӱ��ϵ��

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
