using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class PrefabLoadAsset : BaseAsset
{    
    [OnValueChanged("AddPickGOToAddressable")]
    public GameObject PickGO;
    public string AddressPath;//��Դ·��
    public bool isNeedReLoad;//�Ƿ���Ҫÿ�����¼���
    public bool isParticle;//�Ƿ�������ϵͳ
    public ExposedReference<Transform> parent;
    public Vector3 offset;

    public override BaseBehaviour GetBehaviour(PlayableGraph graph, GameObject go)
    {
        var baseBehaviour = new PrefabLoadBehaviour();
        baseBehaviour.target_ = target_.Resolve(graph.GetResolver());
        baseBehaviour.AddressPath = AddressPath;
        baseBehaviour.isNeedReLoad = isNeedReLoad;
        baseBehaviour.isParticle = isParticle;
        baseBehaviour.offset = offset;
        baseBehaviour.parent = parent.Resolve(graph.GetResolver());
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
