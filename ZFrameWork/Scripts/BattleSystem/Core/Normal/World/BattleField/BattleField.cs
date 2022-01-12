using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleModel
{ 
    Normal =  1,
    ECS = 2,
}

public class BattleField : MonoBehaviour
{
    public BattleModel model = BattleModel.Normal;
    public GameObject Ecs;
    public NormalBattle Normal;
    public GameObject GlobalRoot;
    private static BattleField instance;
    public static BattleField Instance { get { return instance; } }

    public void OnClickKeyCode(KeyCode pKeyCode) {
        Normal.OnClickKeyCode(pKeyCode);
    }
    public int AddDelayAction(float pDelayTime, System.Action pOnAction)
    {
        return Normal.AddDelayAction(pDelayTime, pOnAction);
    }
    public void RemoveDelayAction(int pAcId)
    {
        Normal.RemoveDelayAction(pAcId);
    }
    public void SelectTaragetId(int pTargetId) {
        //targetUnitId = pTargetId;
        Normal.SelectTaragetId(pTargetId);
    }
    public void OnSeletUnitId(int pUnitId,bool pPlayerOwer) {
        Normal.OnSeletUnitId(pUnitId, pPlayerOwer);
        //selectUnitId = pUnitId;
        //playerOwer = pPlayerOwer;
    }
    public void ClickPos(Vector3 pPos) {
        Normal.ClickPos(pPos);
        //clickPos = pPos;
    }

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start(){
        if (model == BattleModel.ECS)
        {
            Ecs.SetActive(true);
        }
        else
        {
            Normal.gameObject.SetActive(true);
        }
    }
}
