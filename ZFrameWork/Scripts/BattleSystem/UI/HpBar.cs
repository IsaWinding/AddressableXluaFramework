using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public float max;
    public Image forward;
    public Text text;
    private float forwardOrWith;
    private float hpBarHeight = 3f;
    private void Awake(){}

    public void SetHeight(float pHeight){
        hpBarHeight = pHeight;
        this.transform.localPosition = Vector3.up * hpBarHeight;
    }

    private void Start(){
        this.transform.localPosition = Vector3.up * hpBarHeight;
    }

    public void SetHp(float pCurHp,float pMaxHp)
    {
        text.text = pCurHp + "/" + pMaxHp;
        var progress = pCurHp / (pMaxHp * 1f);
        var weight = progress * (max);
        forward.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, weight);// = new Vector3(progress * (max), 0, 0);
        forward.rectTransform.localPosition = new Vector3((weight - max) / 2, 0, 0);
    }
    private void LateUpdate()
    {
        this.transform.rotation = Camera.main.transform.rotation;
        //this.transform.LookAt(Camera.main.transform);
    }
}
