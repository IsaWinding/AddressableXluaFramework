using UnityEngine;
using Entitas;

//�Ƿ�����ƶ�
[Battle]
public class CanMoveComponent : IComponent { }
//�ƶ���Ŀ��λ��
[Battle]
public class MoveTargetComponent : IComponent
{
    public Vector3 target;
}
//�Ƿ��ƶ����
[Battle]
public class MoveFinish : IComponent { }
