using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerInput : MonoBehaviour
{
    public string ClickGO;
    private Ray ray;
    private RaycastHit hit;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                var unitTag = hit.collider.gameObject.GetComponent<UnitTag>();
                if (unitTag != null){
                    BattleField.Instance.OnSeletUnitId(unitTag.Id, unitTag.PlayerOwer);
                }
            }
        }
        if (Input.GetMouseButtonDown(1)) {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)){
                var unitTag = hit.collider.gameObject.GetComponent<UnitTag>();
                if (unitTag != null){
                    BattleField.Instance.SelectTaragetId(unitTag.Id);
                }
                else{
                    var point_ = hit.point;
                    point_.y = 0;
                    BattleField.Instance.ClickPos(point_);

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            BattleField.Instance.OnClickKeyCode(KeyCode.Alpha1);
        }
    }
}
