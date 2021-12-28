using System.Collections;
using System.Collections.Generic;
using TimeLine;
using UnityEngine;

public class DmgDrawBehaviour : BaseBehaviour
{
    public float Radius;
    public float Height;
    public float Degree;
    public Color Color;
    private GameObject mRec = null;
    private bool isCanSend = false;
    TimeLine.UDrawTool tool;
    protected override void OnProgress(float pProgress)
    {
        if (pProgress >= 0.1f)
        {
            if (isCanSend)
            {
                if (mRec == null)
                    mRec = new GameObject("temp");
                if (tool == null || tool.go == null)
                {
                    tool = mRec.AddComponent<UDrawTool>();
                    mRec.transform.localScale = Vector3.one;
                    mRec.transform.position = new Vector3(target_.transform.position.x, -1, target_.transform.position.z);
                    mRec.transform.forward = target_.transform.forward;
                    tool.DrawSectorSolid(mRec.transform, mRec.transform.position, Degree, Radius, Color);
                }
                else
                {
                    if(tool.go != null)
                        tool.go.SetActive(true);
                }
                isCanSend = false;
            }
        }
        if (pProgress < 0.1f)
        {
            isCanSend = true;
        }
    }
    protected override void OnEnd()
    {
        if (tool != null)
        {
            tool.go.SetActive(false);
        }
    }
    protected override void OnDestory()
    {
        if (tool != null)
        {
            if(tool.go != null)
                GameObject.DestroyImmediate(tool.go);
            tool = null;
        }
        if (mRec != null)
        {
            GameObject.DestroyImmediate(mRec);
            mRec = null;
        }
    }
}
