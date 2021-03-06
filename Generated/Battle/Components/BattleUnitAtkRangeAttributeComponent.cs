//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class BattleEntity {

    public UnitAtkRangeAttribute unitAtkRangeAttribute { get { return (UnitAtkRangeAttribute)GetComponent(BattleComponentsLookup.UnitAtkRangeAttribute); } }
    public bool hasUnitAtkRangeAttribute { get { return HasComponent(BattleComponentsLookup.UnitAtkRangeAttribute); } }

    public void AddUnitAtkRangeAttribute(AttributeValue newValue) {
        var index = BattleComponentsLookup.UnitAtkRangeAttribute;
        var component = (UnitAtkRangeAttribute)CreateComponent(index, typeof(UnitAtkRangeAttribute));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceUnitAtkRangeAttribute(AttributeValue newValue) {
        var index = BattleComponentsLookup.UnitAtkRangeAttribute;
        var component = (UnitAtkRangeAttribute)CreateComponent(index, typeof(UnitAtkRangeAttribute));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveUnitAtkRangeAttribute() {
        RemoveComponent(BattleComponentsLookup.UnitAtkRangeAttribute);
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

    static Entitas.IMatcher<BattleEntity> _matcherUnitAtkRangeAttribute;

    public static Entitas.IMatcher<BattleEntity> UnitAtkRangeAttribute {
        get {
            if (_matcherUnitAtkRangeAttribute == null) {
                var matcher = (Entitas.Matcher<BattleEntity>)Entitas.Matcher<BattleEntity>.AllOf(BattleComponentsLookup.UnitAtkRangeAttribute);
                matcher.componentNames = BattleComponentsLookup.componentNames;
                _matcherUnitAtkRangeAttribute = matcher;
            }

            return _matcherUnitAtkRangeAttribute;
        }
    }
}
