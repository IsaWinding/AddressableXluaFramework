using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Creat/AIPath")]
public class AIPathSetter : ScriptableObject{
    
    public List<Vector3>  poss;
    public PathType pathType = PathType.Loop;
    public static AIPathSetter LoadDataFromFile(string Path){
        var textAsset = AssetManager.Instance.LoadAsset<AIPathSetter>(Path);
        return textAsset;
    }
    public AIPath GetAIPath(){
        var aiPaths = AIPath.GetPathsByVector3s(poss);
        var aiPath = new AIPath() {Paths = aiPaths,pathType = this.pathType };
        aiPath.Init();
        return aiPath;
    }
    public void SetByGOs(List<GameObject> gos) {
        poss = new List<Vector3>();
        foreach (var temp in gos)
        {
            var pos_ = temp.transform.position;
            pos_.y = 0;
            poss.Add(pos_);
        }
    }
    
}
