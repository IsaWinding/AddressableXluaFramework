//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class BattleEntity {

    public ModelResNameComponent modelResName { get { return (ModelResNameComponent)GetComponent(BattleComponentsLookup.ModelResName); } }
    public bool hasModelResName { get { return HasComponent(BattleComponentsLookup.ModelResName); } }

    public void AddModelResName(string newValue) {
        var index = BattleComponentsLookup.ModelResName;
        var component = (ModelResNameComponent)CreateComponent(index, typeof(ModelResNameComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceModelResName(string newValue) {
        var index = BattleComponentsLookup.ModelResName;
        var component = (ModelResNameComponent)CreateComponent(index, typeof(ModelResNameComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveModelResName() {
        RemoveComponent(BattleComponentsLookup.ModelResName);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class BattleMatcher {

    static Entitas.IMatcher<BattleEntity> _matcherModelResName;

    public static Entitas.IMatcher<BattleEntity> ModelResName {
        get {
            if (_matcherModelResName == null) {
                var matcher = (Entitas.Matcher<BattleEntity>)Entitas.Matcher<BattleEntity>.AllOf(BattleComponentsLookup.ModelResName);
                matcher.componentNames = BattleComponentsLookup.componentNames;
                _matcherModelResName = matcher;
            }

            return _matcherModelResName;
        }
    }
}
