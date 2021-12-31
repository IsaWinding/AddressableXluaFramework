using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager: IDriver
{
    private Dictionary<int, Unit> Units = new Dictionary<int, Unit>();
    private int _id;
    public void AddUnitByAttributeAndState(AttributeC pAttributeInfo,StateInfo pStateInfo){
        _id ++;
        var unit = new Unit(_id, pAttributeInfo, pStateInfo);
        AddUnit(unit);
    }
    public void AddUnit(Unit pUnit) {
        Units.Add(pUnit.Id, pUnit);
    }
    public Unit GetUnit(int pId){
        Unit unit;
        Units.TryGetValue(pId,out unit);
        return unit;
    }
    public void OnRun(float pTime, float pDeltaTime){
        foreach (var temp in Units.Values){
            temp.OnRun(pTime, pDeltaTime);
        }
    }
}
