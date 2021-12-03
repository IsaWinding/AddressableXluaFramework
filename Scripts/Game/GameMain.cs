using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        XLuaClient.Instance.StartMain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
