using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

//单位的阵营
[Battle]
public sealed class CampComponent : IComponent
{
    public CampType value;
}

//当前位置
[Battle]
public sealed class PositionComponent : IComponent{
    public Vector3 value;
}

//移动的方向
[Battle]
public class DirectionComponent : IComponent{
    public float value;
}

//战斗的实例物体
[Battle]
public class ModelComponent : IComponent
{
    public GameObject gameObject;
}
//加载的模型资源
[Battle]
public class ModelResComponent : IComponent
{
    public GameObject gameObject;
}
//模型的资源名称
[Battle]
public class ModelResNameComponent : IComponent{
    public string Value;
}

//模型的动画名称
[Battle]
public class ModelAnimationComponent : IComponent {
    public AniNameType AniType;
    public System.Action onAction; 
}





