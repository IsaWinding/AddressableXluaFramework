using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBattle : MonoBehaviour
{
    private UnitManager unitManager;
    public Driver driver;

    public void OnClickKeyCode(KeyCode pKeyCode)
    {
        unitManager.OnClickKeyCode(pKeyCode);
    }
    public int AddDelayAction(float pDelayTime, System.Action pOnAction)
    {
        return driver.AddDelayAction(pDelayTime, pOnAction);
    }
    public void RemoveDelayAction(int pAcId)
    {
        driver.RemoveDelayAction(pAcId);
    }

    public void SelectTaragetId(int pTargetId)
    {
        unitManager.SelectTaragetId(pTargetId);
    }
    public void OnSeletUnitId(int pUnitId, bool pPlayerOwer)
    {
        unitManager.OnSeletUnitId(pUnitId, pPlayerOwer);
    }
    public void ClickPos(Vector3 pPos)
    {
        unitManager.ClickPos(pPos);
    }
    void Start(){
        unitManager = new UnitManager();
        LoadTestData();
        driver.AddAction(OnRun);
    }
    void LoadTestData(){
        var unitLoaders = this.gameObject.GetComponentsInChildren<UnitLoder>();
        for (var i = 0; i < unitLoaders.Length; i++)
        {
            unitLoaders[i].OnLoad(unitManager);
        }
        unitManager.OnInit();
    }
    private void OnRun(float pTime, float pDeltaTime)
    {
        unitManager.OnRun(pTime, pDeltaTime);
    }
}
