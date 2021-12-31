using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

//��λ����Ӫ
[Battle]
public sealed class CampComponent : IComponent
{
    public CampType value;
}

//��ǰλ��
[Battle]
public sealed class PositionComponent : IComponent{
    public Vector3 value;
}

//�ƶ��ķ���
[Battle]
public class DirectionComponent : IComponent{
    public float value;
}

//ģ�͵�ʵ������
[Battle]
public class ModelComponent : IComponent
{
    public GameObject gameObject;
}
//ģ�͵���Դ����
[Battle]
public class ModelResNameComponent : IComponent{
    public string Value;
}

//ģ�͵Ķ�������
[Battle]
public class ModelAnimationComponent : IComponent {
    public string Name;
}





