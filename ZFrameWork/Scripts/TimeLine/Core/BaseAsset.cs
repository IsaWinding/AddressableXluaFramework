using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using NaughtyAttributes;

public static class PlayableDef {
	public static GameObject FindGO(GameObject root,string relatePath) {

		if (string.IsNullOrEmpty(relatePath))
		{
			return root;
		}
		else {
			string[] split = relatePath.Split('/');
			var cur = root.transform;
			for (int index = 1; index < split.Length; index++)
			{
				var name = split[index];
				cur = cur.Find(name);
				if (cur == null)
				{
					return null;
				}
			}
			return cur.gameObject;
		}
	}
	public static string FindRelatedPath(Transform child, Transform root)
	{
		//Debug.Log("childname:{0}, root:{1}.".F(child.name, root.name));
		var current = child;
		System.Collections.Generic.List<string> pathList = new System.Collections.Generic.List<string>();
		int counter = 0;
		string path = null;
		while (current != null && counter <= 20)
		{
			pathList.Add(current.gameObject.name);
			if (current == root)
			{
				path = ConvertListToPath(pathList);
				return path;
			}
			current = current.parent;
			counter++;
		}
		return path;
	}
	private static string ConvertListToPath(System.Collections.Generic.List<string> pathList)
	{
		string result = "";
		for (int index = pathList.Count - 2; index >= 0; index--)
		{
			if (index == pathList.Count - 2)
				result += pathList[index];
			else
				result += "/" + pathList[index];
		}
			
		return result;
	}
}

[System.Serializable]
public class PlayableAssetGO
{
	[OnValueChanged("OnParentChange")]
	public ExposedReference<GameObject> parent;
		
	public ExposedReference<GameObject> target_;
	public string relaPath;
	public GameObject GetGo(PlayableGraph graph, GameObject go) {
		var go_ = target_.Resolve(graph.GetResolver());
		var parent_ = parent.Resolve(graph.GetResolver());
		if (parent_ != null && go_ != null)
		{
			relaPath = PlayableDef.FindRelatedPath(go_.transform, parent_.transform);
		}
		if (go_ == null) {
			if (!string.IsNullOrEmpty(relaPath)){
				go_ = go.transform.Find(relaPath).gameObject;
			}
			else {
				go_ = go;
			}
		}
		return go_;
	}
	private void SetRelaPath(GameObject pParent,GameObject pTarget) { 
		
		
	}
#if UNITY_EDITOR
	public void OnParentChange()
	{
		if (parent.defaultValue != null && target_.defaultValue!= null)
		{ 
			
		}
	}
#endif

}


public class BaseAsset : PlayableAsset
{
	public PlayableAssetGO target_;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
	{
		var baseBehaviour = GetBehaviour(graph, go);
		return ScriptPlayable<BaseBehaviour>.Create(graph, baseBehaviour);
	}
	public virtual BaseBehaviour GetBehaviour(PlayableGraph graph, GameObject go)
	{
		return null;

	}
}
