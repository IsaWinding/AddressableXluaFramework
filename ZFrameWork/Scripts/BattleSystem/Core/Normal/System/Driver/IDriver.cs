using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDriver
{
    public void OnInit();
    public void OnRun(float pTime,float pDeltaTime);
    public void OnDestory();
}
