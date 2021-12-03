using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowStep 
{
	private int stepId;
	private System.Action<System.Action<bool>> action_;

	public FlowStep(int pStepId,System.Action<System.Action<bool>> pAction)
	{
		stepId = pStepId;
		action_ = pAction;
	}
	public void RunAction(System.Action<bool> pFinish)
	{
		action_.Invoke(pFinish);
	}
}
