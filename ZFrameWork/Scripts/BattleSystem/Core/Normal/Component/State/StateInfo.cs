using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInfo{
    public Vector3 bornPos;
    public CampType campType;

    public string Model;
    public string HpBar;
    public float modelHeight;

    public Vector3 GetPos()
    {
        return bornPos;
    }
    public StateInfo(Vector3 pBornPos,CampType pCampType) {
        campType = pCampType;
        bornPos = pBornPos;
    }
}
