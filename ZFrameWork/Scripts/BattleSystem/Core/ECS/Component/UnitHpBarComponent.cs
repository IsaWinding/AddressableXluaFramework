using UnityEngine;
using Entitas;

//ģ�͵�ʵ������
[Battle]
public class UnitHpBarComponent : IComponent
{
    public GameObject gameObject;
}

[Battle]
public class UnitHpBarResNameComponent : IComponent {
    public string resName;
}
