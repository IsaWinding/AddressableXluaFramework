using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Image forward;
    public Text text;
    public void SetHp(float pCurHp,float pMaxHp)
    {
        text.text = pCurHp + "/" + pMaxHp;
    }
    private void LateUpdate()
    {
        this.transform.rotation = Camera.main.transform.rotation;
        //this.transform.LookAt(Camera.main.transform);
    }
}
