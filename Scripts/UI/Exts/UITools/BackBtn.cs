using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackBtn : MonoBehaviour
{
    private void Awake()
    {
        var btn = this.gameObject.GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(() => {
                UICtrlManager.CloseTopBaseUI();
            });
        }
    }
}
