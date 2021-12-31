using UnityEngine;
using Entitas;

//模型的实例物体
[Battle]
public class UnitHpBarComponent : IComponent
{
    public GameObject gameObject;
}

[Battle]
public class UnitHpBarResNameComponent : IComponent {
    public string resName;
}
