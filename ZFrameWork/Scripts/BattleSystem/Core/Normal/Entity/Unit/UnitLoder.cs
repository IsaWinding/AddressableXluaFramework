using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLoder : MonoBehaviour
{
    public UnitSetter setter;
    public bool LoopLoader;
    public float DelayTime = 5;
    public float RepeatTiem = 30;
    public bool PlayerOwer = false;//玩家自己是否拥有控制权限
    private UnitManager unitManager;

    private void Start(){
        if (LoopLoader){
            InvokeRepeating("OnLoad", DelayTime, RepeatTiem);
        }
        else{
            Invoke("OnLoad", DelayTime);
        }
    }
    private void OnLoad(){
        var unit = setter.GetUnit(unitManager.NewId());
        unit.SetPlayerOwer(PlayerOwer);
        unitManager.AddUnit(unit);
    }
    public void OnLoad(UnitManager pUnitManager)
    {
        unitManager = pUnitManager;
    }
}
