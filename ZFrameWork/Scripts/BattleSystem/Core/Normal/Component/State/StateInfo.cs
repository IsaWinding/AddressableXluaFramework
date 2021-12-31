using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInfo{
    public float x;
    public float y;
    public float z;
    public CampType campType;
    public string Model;
    public StateInfo(float pX,float pY,float pZ,CampType pCampType) {
        campType = pCampType;
        x = pX;
        y = pY;
        z = pZ;
    }
}
