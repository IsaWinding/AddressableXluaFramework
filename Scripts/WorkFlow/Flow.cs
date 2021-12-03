using System.Collections.Generic;
public class Flow 
{
	private Dictionary<int, FlowStep> steps = new Dictionary<int, FlowStep>();
	private int FlowId;
	private int curStep = 0;
	private int maxStep = 0;
	public Flow(int pFlowId)
	{
		FlowId = pFlowId;
	}
	public void RunAllStep(System.Action<bool> pFinish)
	{
		curStep = 0;
		LoopRunAllStep(pFinish);
	}
	private void LoopRunAllStep(System.Action<bool> pFinish)
	{
		curStep++;
		if (curStep > maxStep)
		{
			pFinish.Invoke(true);
		}
		else
		{
			if (steps.ContainsKey(curStep))
			{
				steps[curStep].RunAction((oNext)=> {
					if (oNext)
					{
						LoopRunAllStep(pFinish);
					}
					else
					{
						pFinish.Invoke(false);
					}
				});
			}
			else
			{
				LoopRunAllStep(pFinish);
			}
		}
	}
	public void AddStep(int pStepId,System.Action<System.Action<bool>> pAction)
	{
		var flowStep = new FlowStep(pStepId, pAction);
		if (!steps.ContainsKey(pStepId))
		{
			steps.Add(pStepId,flowStep);
		}
		if (pStepId > maxStep)
		{
			maxStep = pStepId;
		}
	}
}
