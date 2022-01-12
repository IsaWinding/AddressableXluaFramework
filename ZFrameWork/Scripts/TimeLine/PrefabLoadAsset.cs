using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class PrefabLoadAsset : BaseAsset
{    
    [OnValueChanged("AddPickGOToAddressable")]
    public GameObject PickGO;
    public string AddressPath;//资源路径
    public bool isNeedReLoad;//是否需要每次重新加载
    public bool isParticle;//是否是粒子系统

    public PlayableAssetGO parent;
    public Vector3 offset;

    public override BaseBehaviour GetBehaviour(PlayableGraph graph, GameObject go)
    {
        var baseBehaviour = new PrefabLoadBehaviour();
        baseBehaviour.target_ = target_.GetGo(graph, go);
        baseBehaviour.AddressPath = AddressPath;
        baseBehaviour.isNeedReLoad = isNeedReLoad;
        baseBehaviour.isParticle = isParticle;
        baseBehaviour.offset = offset;
        baseBehaviour.parent = parent.GetGo(graph, go).transform;
        return baseBehaviour;
    }
#if UNITY_EDITOR
    public void AddPickGOToAddressable()
    {
        if (PickGO != null)
        {
            AddressPath = UnityEditor.AssetDatabase.GetAssetPath(PickGO);
        }
    }
#endif
}
