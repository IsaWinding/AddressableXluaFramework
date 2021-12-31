using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleField : MonoBehaviour
{
    private UnitManager unitManager;
    public Driver driver;
    // Start is called before the first frame update
    void Start(){
        unitManager = new UnitManager();
        LoadTestData();
        driver.AddAction(OnRun);
    }
    void LoadTestData() {
        var unitLoaders = this.gameObject.GetComponentsInChildren<UnitLoder>();
        for (var i = 0; i < unitLoaders.Length; i++)
        {
            unitLoaders[i].OnLoad(unitManager);
        }
    }
    private void OnRun(float pTime,float pDeltaTime){
        unitManager.OnRun(pTime, pDeltaTime);
    }

}
