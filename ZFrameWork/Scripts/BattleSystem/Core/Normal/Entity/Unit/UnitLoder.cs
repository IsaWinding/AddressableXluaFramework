using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLoder : MonoBehaviour
{
    public UnitSetter setter;
    public void OnLoad(UnitManager pUnitManager)
    {
        pUnitManager.AddUnitByAttributeAndState(setter.attributeSetter.GetAttributeInfo(), 
            setter.stateSetter.GetStateInfo());
    }
}
