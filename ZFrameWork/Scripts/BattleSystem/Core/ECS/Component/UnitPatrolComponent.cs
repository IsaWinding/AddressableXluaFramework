using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

[Battle]
public class UnitPartrolEnable : IComponent { 
    
}
[Battle]
public class UnitPartrolType : IComponent
{
    public PathType pathType;
}
[Battle]
public class UnitPartrolPoint : IComponent
{
    public bool isForward = true;
    public PathPoint nextPoint;
}
[Battle]
public class UnitPartrolPathComponent : IComponent {

    public List<PathPoint> Paths = new List<PathPoint>();
}


